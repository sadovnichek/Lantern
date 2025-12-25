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
            await connection.ConnectAsync("149.154.167.50", 443);

            byte[] nonce = RandomNumberGenerator.GetBytes(16);
            var tl = new TLWriter();
            tl.WriteInt(0x60469778);
            tl.WriteRaw(nonce);
            var payload = tl.GetBytes();

            var stream = new MemoryStream();
            var authKeyId = new byte[8];
            stream.Write(authKeyId);
            var id = DateTimeOffset.UtcNow.ToUnixTimeSeconds() << 32;
            stream.Write(BitConverter.GetBytes(id), 0, 8);
            stream.Write(BitConverter.GetBytes(payload.Length));
            stream.Write(payload);

            var packet = stream.ToArray();

            await connection.SendAsync(packet);

            var received = await connection.ReceiveAsync();
            Console.WriteLine(received);
        }
    }
}