using System;
using System.Buffers;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace TelegramAPI
{
    public class Connection : IDisposable
    {
        private readonly TcpClient client;
        private NetworkStream stream;

        public Connection(string ip, int port)
        {
            client = new TcpClient(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public Connection()
        {
            client = new TcpClient();
        }

        public async Task ConnectAsync(string host, int port)
        {
            try
            {
                await client.ConnectAsync(host, port);
                stream = client.GetStream();
#if DEBUG
                Console.WriteLine($"Connected to {host}:{port}");
#endif
            }
            catch (Exception)
            {
#if DEBUG
                Console.WriteLine($"Unable to connect to {host}:{port}");
#endif
                throw;
            }
        }

        public async Task SendAsync(byte[] data)
        {
            await stream.WriteAsync(data);
        }

        public async Task<byte[]> ReceiveAsync()
        {
            var responseData = new byte[1024];
            var response = new MemoryStream();
            int bytes;

            do
            {
                bytes = await stream.ReadAsync(responseData);
                response.Write(responseData, 0, bytes);
            }
            while (client.Available > 0);

            return response.ToArray();
        }

        public void Close()
        {
            stream.Close();
            client.Close();
        }

        public async void Dispose()
        {
            client.Dispose();
        }
    }
}