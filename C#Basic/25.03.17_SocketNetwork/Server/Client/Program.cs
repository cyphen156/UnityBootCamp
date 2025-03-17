using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Schema;

namespace Client
{
    internal class Program
    {
        const int BUFSIZE = 2400;
        static void Main(string[] args)
        {
            
            for (int i = 0; i < 10; ++i)
            {
                /// C Style Example
                //int c_socket;
                //int n;
                //c_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
                //memset(&s_addr, 0, sizeof(s_addr));
                //s_addr.sin_addr.s_addr = inet_addr(IPADDR);
                //s_addr.sin_family = AF_INET;
                //s_addr.sin_port = htons(PORT);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //struct sockaddr_in s_addr;
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
                // IPAddress.Loopback

                serverSocket.Connect(serverEndPoint);
                //if (connect(c_socket, (struct sockaddr*)&s_addr, sizeof(s_addr)) == -1){
                //    printf("Can not connect!!!\n");
                //    close(c_socket);
                //    return -1;
                //}
                IPEndPoint localEndPoint = (IPEndPoint)serverSocket.LocalEndPoint;
                Console.WriteLine($"[클라이언트 {i + 1}] 서버에 연결됨 (내 포트: {localEndPoint.Port} → 서버 포트: {serverEndPoint.Port})");

                // char sndBuffer[BUFSIZ], rcvBuffer[BUFSIZ];
                byte[] buffer = new byte[BUFSIZE];
                byte[] buffer2 = new byte[BUFSIZE];

                string message = "hello World";

                buffer = Encoding.UTF8.GetBytes(message);

                int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                if (SendLength < buffer.Length)
                {
                    SendLength = serverSocket.Send(buffer, SendLength, buffer.Length, SocketFlags.None);
                }
                int RecvLength = serverSocket.Receive(buffer2);
                Console.WriteLine($"[클라이언트 {i + 1}] 서버 응답: {Encoding.UTF8.GetString(buffer2, 0, RecvLength)}");

                Console.WriteLine(Encoding.UTF8.GetString(buffer2));

                //while (1)
                //{
                //    memset(sndBuffer, 0, BUFSIZ);
                //    printf("Input message to send to server.\n");
                //    printf("if you want to quit, type quit.\n");
                //    if ((n = read(0, sndBuffer, BUFSIZ)) > 0)
                //    {
                //        sndBuffer[n] = '\0';
                //        if (!strcmp(sndBuffer, "quit\n"))
                //            break;
                //        printf("original Data : %s", sndBuffer);
                //        if ((n = write(c_socket, sndBuffer, strlen(sndBuffer))) < 0)
                //        {
                //            return -1;
                //        }
                //        memset(rcvBuffer, 0, BUFSIZ);
                //        if ((n = read(c_socket, rcvBuffer, BUFSIZ)) < 0)
                //        {
                //            return -1;
                //        }

                //        printf("echoed Data : %s", rcvBuffer);
                //    }
                //}
                serverSocket.Close();
                //close(c_socket);
            }
        }
    }
}
