using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Network.Socket
{
    public static class TCPClientExtension
    {
        public static bool Connect(this TcpClient client, string ip, int port, int timeoutSeconds)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(BeginConnect));
            thread.IsBackground = true;
            thread.Start(new Tuple<TcpClient, string, int>(client, ip, port));
            thread.Join(timeoutSeconds * 1000);
            return client.Connected;
        }

        private static void BeginConnect(object obj)
        {
            Tuple<TcpClient, string, int> target = obj as Tuple<TcpClient, string, int>;
            try
            {
                target.Item1.Connect(target.Item2, target.Item3);
            }
            catch { }
        }
    }
}