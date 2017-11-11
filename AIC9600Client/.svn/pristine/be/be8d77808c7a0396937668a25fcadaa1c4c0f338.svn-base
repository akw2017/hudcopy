using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Network.WCF
{
    public class WCFCaller<T>
    {
        public static object ExecuteMethod(string url, string methodName, params object[] args)
        {
            return ExecuteMethod(url, methodName, null, args);
        }

        public static object ExecuteMethod(string url, string methodName, WCFTimeout timeout, params object[] args)
        {
            EndpointAddress address = new EndpointAddress(url);
            Binding bindinginstance = null;

            if (url.StartsWith("http"))
            {
                BasicHttpBinding http = new BasicHttpBinding();
                http.CloseTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.CloseTimeoutSeconds);
                http.OpenTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.OpenTimeoutSeconds);
                http.ReceiveTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.ReceiveTimeoutSeconds);
                http.SendTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.SendTimeoutSeconds);
                http.MaxBufferPoolSize = long.MaxValue;
                http.MaxBufferSize = int.MaxValue;
                http.MaxReceivedMessageSize = long.MaxValue;
                http.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxDepth = int.MaxValue, MaxArrayLength = int.MaxValue, MaxBytesPerRead = int.MaxValue, MaxNameTableCharCount = int.MaxValue, MaxStringContentLength = int.MaxValue };
                http.TransferMode = TransferMode.Streamed;
                http.Security.Mode = BasicHttpSecurityMode.None;
                bindinginstance = http;

            }
            else if (url.StartsWith("net.tcp"))
            {
                NetTcpBinding tcp = new NetTcpBinding();
                tcp.CloseTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.CloseTimeoutSeconds);
                tcp.OpenTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.OpenTimeoutSeconds);
                tcp.ReceiveTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.ReceiveTimeoutSeconds);
                tcp.SendTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.SendTimeoutSeconds);
                tcp.MaxBufferPoolSize = long.MaxValue;
                tcp.MaxBufferSize = int.MaxValue;
                tcp.MaxConnections = 32768;
                tcp.MaxReceivedMessageSize = long.MaxValue;
                tcp.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxDepth = int.MaxValue, MaxArrayLength = int.MaxValue, MaxBytesPerRead = int.MaxValue, MaxNameTableCharCount = int.MaxValue, MaxStringContentLength = int.MaxValue };
                tcp.TransferMode = TransferMode.Streamed;
                tcp.Security.Mode = SecurityMode.None;
                tcp.TransactionFlow = false;
                bindinginstance = tcp;
            }
            else if (url.StartsWith("net.pipe"))
            {
                NetNamedPipeBinding pipe = new NetNamedPipeBinding();
                pipe.CloseTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.CloseTimeoutSeconds);
                pipe.OpenTimeout = TimeSpan.FromSeconds(timeout == null ? 10 : timeout.OpenTimeoutSeconds);
                pipe.ReceiveTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.ReceiveTimeoutSeconds);
                pipe.SendTimeout = TimeSpan.FromSeconds(timeout == null ? 30 : timeout.SendTimeoutSeconds);
                pipe.MaxBufferPoolSize = long.MaxValue;
                pipe.MaxBufferSize = int.MaxValue;
                pipe.MaxConnections = 32768;
                pipe.MaxReceivedMessageSize = long.MaxValue;
                pipe.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxDepth = int.MaxValue, MaxArrayLength = int.MaxValue, MaxBytesPerRead = int.MaxValue, MaxNameTableCharCount = int.MaxValue, MaxStringContentLength = int.MaxValue };
                pipe.TransferMode = TransferMode.Streamed;
                pipe.Security.Mode = NetNamedPipeSecurityMode.None;
                pipe.TransactionFlow = false;
                bindinginstance = pipe;
            }
            else return null;

            using (ChannelFactory<T> channel = new ChannelFactory<T>(bindinginstance, address))
            {
                foreach (var operation in channel.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = operation.Behaviors[typeof(DataContractSerializerOperationBehavior)] as DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }

                T instance = channel.CreateChannel();
                try
                {
                    Type type = typeof(T);
                    MethodInfo info = type.GetMethod(methodName);
                    if (info == null)
                    {
                        foreach (Type interfaceType in type.GetInterfaces())
                        {
                            info = interfaceType.GetMethod(methodName);
                            if (info != null) break;
                        }
                    }
                    object result = null;
                    if (info != null)
                    {
                        result = info.Invoke(instance, args);
                    }
                    (instance as ICommunicationObject).Close();
                    return result;
                }
                catch (Exception ex)
                {
                    if (instance != null && (instance as ICommunicationObject).State == System.ServiceModel.CommunicationState.Faulted)
                    {
                        (instance as ICommunicationObject).Abort();
                    }
                    else if (instance != null && (instance as ICommunicationObject).State != System.ServiceModel.CommunicationState.Closed)
                    {
                        (instance as ICommunicationObject).Close();
                    }
                    throw ex;
                }
            }
        }
    }
}