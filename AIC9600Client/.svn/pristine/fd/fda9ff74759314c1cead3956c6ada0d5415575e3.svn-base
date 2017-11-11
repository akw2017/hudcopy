using CitizenSoftwareLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Network.Socket
{
    public delegate void ServerMessageReceivedHandler(TCPContext obj, TCPMessage message);
    public delegate void ServerDisconnectedHandler(TCPContext obj);
    public delegate void ServerConnectedHandler(TCPContext obj);

    public class TCPClient : IDisposable
    {
        private TCPContext _currentContext = null;
        public TCPContext CurrentContext
        {
            get { return _currentContext; }
        }

        private Thread _autoReconnectionThread = null;

        public event ServerMessageReceivedHandler ServerMessageReceived = null;
        public event ServerDisconnectedHandler ServerDisconnected = null;
        public event ServerConnectedHandler ServerConnected = null;

        public int Port { get; private set; }
        public string IPAddress { get; private set; }
        public bool UseTLS { get; private set; }
        public string ClientCertPath { get; private set; }
        public string ClientCertPassword { get; private set; }
        public string ServerHostName { get; private set; }
        public bool AutoReconnection { get; private set; }

        public bool Connected
        {
            get
            {
                if (_currentContext != null)
                {
                    return _currentContext.Client.Connected;
                }
                return false;
            }
        }


        private Dictionary<string, SyncController> _responseWattings = new Dictionary<string, SyncController>();

        public TCPClient(string ipAddress, int port, bool autoReconnection, bool useTLS = false, string serverHostName = null, string clientCertPath = null, string clientCertPassword = null)
        {
            this.UseTLS = useTLS;
            this.ClientCertPath = clientCertPath;
            this.ClientCertPassword = clientCertPassword;
            this.IPAddress = ipAddress;
            this.Port = port;
            this.ServerHostName = serverHostName;
            this.AutoReconnection = autoReconnection;
        }

        #region Connection
        private void ThreadConnect(object obj)
        {
            bool repeatconnect = (bool)obj;
            TcpClient client = new TcpClient();
            client.ReceiveBufferSize = 8192;
            client.SendBufferSize = 8192;
            TCPContext context = null;
            try
            {
                if (repeatconnect)
                {
                    while (true)
                    {
                        try
                        {
                            client.Connect(IPAddress, Port, 5000);
                            if (client.Connected) break;
                        }
                        catch { }
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    try
                    {
                        client.Connect(IPAddress, Port);
                    }
                    catch (Exception ex)
                    {
                        LogFactory.Get().Error(ex.ToString());
                        return;
                    }
                }

                if (UseTLS)
                {
                    SslStream stream = new SslStream(client.GetStream(), false);

                    if (!string.IsNullOrWhiteSpace(ClientCertPath) && File.Exists(ClientCertPath))
                    {
                        if (string.IsNullOrWhiteSpace(ClientCertPassword))
                        {
                            stream.AuthenticateAsClient(ServerHostName, new X509CertificateCollection(new X509Certificate[] { new X509Certificate2(ClientCertPath) }), System.Security.Authentication.SslProtocols.Tls, false);
                        }
                        else
                        {
                            stream.AuthenticateAsClient(ServerHostName, new X509CertificateCollection(new X509Certificate[] { new X509Certificate2(ClientCertPath, ClientCertPassword) }), System.Security.Authentication.SslProtocols.Tls, false);
                        }
                    }
                    else
                    {
                        stream.AuthenticateAsClient(ServerHostName);
                    }

                    if (stream.IsAuthenticated)
                    {
                        context = new TCPContext(client, stream);
                        if (ServerConnected != null)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                ServerConnected(context);
                            });
                        }

                        ReadMessage(context);
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    context = new TCPContext(client, client.GetStream());
                    if (ServerConnected != null)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            ServerConnected(context);
                        });
                    }
                    ReadMessage(context);
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
            catch (Exception ex)
            {
                LogFactory.Get().Error(ex.ToString());
            }
            finally
            {
                lock (this)
                {
                    if (context != null)
                    {
                        _currentContext = context;
                    }
                    _autoReconnectionThread = null;
                }
            }
        }

        private void Reconnect(TCPContext context)
        {
            lock (this)
            {
                if (ServerDisconnected != null)
                {
                    Task.Factory.StartNew(() =>
                    {
                        ServerDisconnected(context);
                    });
                }
                if (AutoReconnection && _autoReconnectionThread == null)
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                    LogFactory.Get().DebugFormat("Reconnect Server = {0}:{1}", IPAddress, Port);
                    _autoReconnectionThread = new Thread(new ParameterizedThreadStart(ThreadConnect));
                    _autoReconnectionThread.IsBackground = true;
                    _autoReconnectionThread.Start(true);
                }
            }
        }

        public void Connect()
        {
            lock (this)
            {
                if (AutoReconnection)
                {
                    if (_autoReconnectionThread == null)
                    {
                        if (_currentContext != null)
                        {
                            _currentContext.Dispose();
                        }
                        _autoReconnectionThread = new Thread(new ParameterizedThreadStart(ThreadConnect));
                        _autoReconnectionThread.IsBackground = true;
                        _autoReconnectionThread.Start(true);
                    }
                }
                else
                {
                    ThreadConnect(false);
                }
            }
        }
        #endregion

        #region Message
        private void MessageRead(IAsyncResult obj)
        {
            TCPContext context = obj.AsyncState as TCPContext;
            List<TCPMessage> messages = null;
            int count = 0;
            try
            {
                count = context.Stream.EndRead(obj);
            }
            catch
            {
                Reconnect(context);
                return;
            }
            if (count != 0)
            {
                context.RetryCount = 5;
                try
                {
                    context.AcceptBuffer(count);
                    messages = context.ExtractMessage();
                    if (messages != null && messages.Count != 0)
                    {
                        foreach (var message in messages)
                        {
                            SyncController controller = null;
                            lock (this)
                            {
                                if (_responseWattings.ContainsKey(message.Session))
                                {
                                    controller = _responseWattings[message.Session];
                                }
                            }

                            if(controller != null)
                            {
                                controller.Message = message;
                                controller.Event.Set();
                                if (controller.RaiseReceivedEvent && ServerMessageReceived != null)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        ServerMessageReceived(context, message);
                                    });
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Reconnect(context);
                    return;
                }
            }
            else
            {
                context.RetryCount--;
            }

            if (context.RetryCount > 0)
            {
                try
                {
                    if (context != null && !context.IsClosed && context.Client.Connected)
                    {
                        context.Stream.BeginRead(context.Buffer, 0, context.Buffer.Length, new AsyncCallback(MessageRead), context);
                    }
                }
                catch
                {
                    Reconnect(context);
                    return;
                }
            }
            else
            {
                Reconnect(context);
                return;
            }
        }

        private void ReadMessage(TCPContext context)
        {
            try
            {
                if (context != null && !context.IsClosed && context.Client.Connected)
                {
                    context.Stream.BeginRead(context.Buffer, 0, context.Buffer.Length, new AsyncCallback(MessageRead), context);
                }
            }
            catch
            {
                Reconnect(context);
            }
        }

        #endregion

        public bool Send(TCPMessage message, TimeSpan? timeout = null)
        {
            WaittingClientConnected(timeout);
            
            lock (this)
            {
                if (_currentContext != null && !_currentContext.IsClosed && _currentContext.Client.Connected)
                {
                    try
                    {
                        message.WriteToStream(_currentContext.Stream);
                        return true;
                    }
                    catch { }
                    return false;
                }
                return false;
            }
        }

        public TCPMessage Request(TCPMessage message, TimeSpan? timeout = null, bool raiseReceivedEvent = false)
        {
            WaittingClientConnected(timeout);

            SyncController controller = null;
            lock (this)
            {
                if (_currentContext != null && !_currentContext.IsClosed && _currentContext.Client.Connected)
                {
                    controller = new SyncController(raiseReceivedEvent);
                    _responseWattings.Add(message.Session, controller);
                    try
                    {
                        message.WriteToStream(_currentContext.Stream);
                    }
                    catch { }
                }
            }

            if (controller != null)
            {
                bool set = false;
                if (timeout.HasValue)
                {
                    set = controller.Event.WaitOne(timeout.Value);
                }
                else
                {
                    set = controller.Event.WaitOne(TimeSpan.FromSeconds(30));
                }

                if (set)
                {
                    lock (this)
                    {
                        _responseWattings.Remove(message.Session);
                    }
                    return controller.Message;
                }
                else throw new TimeoutException();
            }
            return null;
        }

        private void WaittingClientConnected(TimeSpan? timeout = null)
        {
            if (_currentContext == null || _currentContext.Client == null || !_currentContext.Client.Connected)
            {
                DateTime deadline = timeout.HasValue ? DateTime.Now.Add(timeout.Value) : DateTime.Now.AddSeconds(30);
                while (DateTime.Now <= deadline)
                {
                    Thread.Sleep(50);
                    if (_currentContext != null && _currentContext.Client != null && _currentContext.Client.Connected)
                    {
                        break;
                    }
                }
            }
        }

        public void Close()
        {
            lock (this)
            {
                AutoReconnection = false;
                if (_autoReconnectionThread != null)
                {
                    _autoReconnectionThread.Abort();
                    _autoReconnectionThread = null;
                }
                if (_currentContext != null)
                {
                    _currentContext.Dispose();
                    _currentContext = null;
                }
            }
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}