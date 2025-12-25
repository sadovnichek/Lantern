using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramAPI
{
    public class TLWriter : IWriter
    {
        private readonly MemoryStream stream = new MemoryStream();

        public byte[] GetBytes()
        {
            return stream.ToArray();
        }

        public void WriteInt(int value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }

        public void WriteString(string value)
        {
            stream.Write(Encoding.UTF8.GetBytes(value));
        }

        public void WriteRaw(byte[] data)
        {
            if (data.Length < 254)
                stream.WriteByte((byte)data.Length);
            else
            {
                stream.WriteByte(254);
                stream.Write(BitConverter.GetBytes(data.Length), 0, 3);
            }

            stream.Write(data);

            while (stream.Length % 4 != 0)
            {
                stream.WriteByte(0);
            }
        }
    }
}
