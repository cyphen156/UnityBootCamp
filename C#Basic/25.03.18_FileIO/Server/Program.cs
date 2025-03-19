using System.Net.Sockets;
using System.Net;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            Socket clientSocket = listenSocket.Accept();

            FileStream fsInput = new FileStream("1.webp", FileMode.Open);

            byte[] buffer = new byte[3];

            int ReadSize = 0;
            do
            {
                ReadSize = fsInput.Read(buffer, 0, buffer.Length);
                int SendSize = clientSocket.Send(buffer, ReadSize, SocketFlags.None);

            } while (ReadSize > 0);

            fsInput.Close();

            clientSocket.Close();
            listenSocket.Close();
        }
    }
}
