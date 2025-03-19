using System.Net.Sockets;
using System.Net;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);

            clientSocket.Connect(listenEndPoint);

            FileStream fsOutput = new FileStream("1_copy.webp", FileMode.Create);
            int RecvLength = 0;
            do
            {
                byte[] buffer = new byte[4096 * 4 * 10];
                RecvLength = clientSocket.Receive(buffer);
                fsOutput.Write(buffer, 0, RecvLength);
            } while (RecvLength > 0);

            clientSocket.Close();
            fsOutput.Close();
        }
    }
}
