using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Schema;

namespace Client2
{
    internal class Program
    {
        const int BUFSIZE = 1024;
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

            serverSocket.Connect(serverEndPoint);
            
            IPEndPoint localEndPoint = (IPEndPoint)serverSocket.LocalEndPoint;
            Console.WriteLine($"[클라이언트] 서버에 연결됨 (내 포트: {localEndPoint.Port} → 서버 포트: {serverEndPoint.Port})");

            byte[] sendBuffer = new byte[BUFSIZE];
            byte[] recvBuffer = new byte[BUFSIZE];

            string message = "I100 + 200";

            sendBuffer = Encoding.UTF8.GetBytes(message);

            int SendLength = serverSocket.Send(sendBuffer, 0, sendBuffer.Length, SocketFlags.None);
            if (SendLength < sendBuffer.Length)
            {
                SendLength = serverSocket.Send(sendBuffer, SendLength, sendBuffer.Length, SocketFlags.None);
            }
            int RecvLength = serverSocket.Receive(recvBuffer);
            Console.WriteLine($"[클라이언트2] 서버 응답: {Encoding.UTF8.GetString(recvBuffer, 0, RecvLength)}");

            Console.WriteLine(Encoding.UTF8.GetString(recvBuffer));

            serverSocket.Close();
        }
    }
}
