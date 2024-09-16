using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    class Program
    {
        static TcpClient client;
        static NetworkStream stream;

        static void Main(string[] args)
        {
            Console.Write("Введите IP адрес сервера: ");
            string serverIp = Console.ReadLine();

            client = new TcpClient(serverIp, 5555);
            stream = client.GetStream();

            Console.WriteLine("Подключено к серверу.");
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                SendMessage(message);
            }
        }

        static void SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }

        static void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int byteCount;

            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Console.WriteLine(message);
            }
        }
    }
}
