using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramAPI
{
    public interface IWriter
    {
        void WriteInt(int value);

        void WriteString(string value);

        byte[] GetBytes();

        void WriteRaw(byte[] data);
    }
}
