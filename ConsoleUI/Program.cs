using System.Security.Cryptography;
using System.Text;
using TelegramAPI;

namespace ConsoleUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var connection = new Connection();
            await connection.ConnectAsync("149.154.167.40", 443);

            var nonce = RandomNumberGenerator.GetBytes(16);
            var req = new byte[4] { 0xf1, 0x8e, 0x7e, 0xbe };
            var tl = new TLWriter();
            tl.WriteRaw(req);
            tl.WriteRaw(nonce);
            var payload = tl.GetBytes();

            var stream = new MemoryStream();
            var authKeyId = new byte[8];
            stream.Write(authKeyId);
            var messageId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() << 32;

            stream.Write(BitConverter.GetBytes(messageId));
            stream.Write(BitConverter.GetBytes(20));
            stream.Write(payload);

            var packet = stream.ToArray();
            Console.WriteLine(TLWriter.GetStringRepresentation(packet));

            await connection.SendAsync(packet);

            var received = await connection.ReceiveAsync();
            Console.WriteLine(received);
        }
    }
}