using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenSoftwareLib.Common
{
    public class GzipOperator
    {
        public static byte[] Compress(byte[] data)
        {
            MemoryStream result = new MemoryStream();
            GZipStream stream = null;
            try
            {
                stream = new GZipStream(result, CompressionMode.Compress);
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
                return result.ToArray();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (stream != null) stream.Close(); stream.Dispose();
                if (result != null) result.Close(); result.Dispose();
            }

            throw new NotImplementedException();
        }

        public static byte[] Decompress(byte[] data)
        {
            return Decompress(data, 0);
        }

        public static byte[] Decompress(byte[] data,int index)
        {
            MemoryStream memStream = new MemoryStream(data,index,data.Length - index);
            memStream.Position = 0;
            MemoryStream result = new MemoryStream();
            GZipStream zipStream = null;
            try
            {
                zipStream = new GZipStream(memStream, CompressionMode.Decompress, true);
                byte[] buffer = new byte[1024];
                int len = 0;
                while ((len = zipStream.Read(buffer, 0, 1024)) > 0)
                {
                    result.Write(buffer, 0, len);
                }
                return result.ToArray();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (zipStream != null) zipStream.Close(); zipStream.Dispose();
                if (memStream != null) memStream.Close(); memStream.Dispose();
                if (result != null) result.Close(); result.Dispose();
            }
        }
    }
}
