using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    class UdpDiscovery
    {
        public static void StartBroadcasting()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 37020);
            byte[] message = Encoding.UTF8.GetBytes("SERVER_DISCOVERY");

            while (true)
            {
                udpClient.Send(message, message.Length, endPoint);
                Thread.Sleep(1000);
            }
        }
    }
}
