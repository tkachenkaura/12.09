using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5555);
            server.Start();
            Console.WriteLine("Сервер запущен...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Новый клиент подключен.");

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteCount;

            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Console.WriteLine(message);
                BroadcastMessage(message, client);
            }

            clients.Remove(client);
            client.Close();
        }

        static void BroadcastMessage(string message, TcpClient senderClient)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            foreach (TcpClient client in clients)
            {
                if (client != senderClient)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
