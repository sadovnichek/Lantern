using System.Text;
using TelegramAPI;

namespace ConsoleUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var connection = new Connection();
            await connection.ConnectAsync("192.169.0.1", 80);

            var requestMessage = $"GET / HTTP/1.1\r\nHost: www.google.com\r\nConnection: Close\r\n\r\n";
            var requestData = Encoding.UTF8.GetBytes(requestMessage);
            await connection.SendAsync(requestData);

            var received = await connection.ReceiveAsync();
            Console.WriteLine(received);
        }
    }
}