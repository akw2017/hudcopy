using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Network.Socket
{
    public static class StreamExtension
    {
        public static void Write(this Stream stream, byte[] datas)
        {
            stream.Write(datas, 0, datas.Length);
        }

        public static byte[] Read(this Stream stream, int count)
        {
            MemoryStream ms = new MemoryStream(count);
            int retrycount = 5;
            byte[] buffer = new byte[count < 8192 ? count : 8192];
            while (retrycount > 0 && ms.Length != count)
            {
                int needcount = count - (int)ms.Length >= buffer.Length ? buffer.Length : count - (int)ms.Length;
                int readcount = stream.Read(buffer, 0, needcount);
                if (readcount == 0)
                {
                    if (ms.Length != count)
                    {
                        retrycount--;
                    }
                    else break;
                }
                else
                {
                    ms.Write(buffer, 0, readcount);
                    if (ms.Length == count) break;
                }
            }
            if (ms.Length != count) return null;
            else return ms.ToArray();
        }
    }
}
