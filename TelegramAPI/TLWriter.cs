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

        public void WriteInt(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(BitConverter.GetBytes(value));
        }

        public void WriteString(string value)
        {
            stream.Write(Encoding.UTF8.GetBytes(value));
        }

        public void WriteRaw(byte[] data)
        {
            stream.Write(data);
        }

        public static string GetStringRepresentation(byte[] bytes)
        {
            return string.Join(" ", bytes.Select(b => b.ToString("X2")));
        }
    }
}
