using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int BUFSIZE = 2445; 
            /// C Style Example
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //s_socket = socket(AF_INET, SOCK_STREAM, IPPORTO_TCP);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 3000);
            //memset(&s_addr, 0, sizeof(s_addr));
            //s_addr.sin_addr.s_addr = htonl(INADDR_ANY);
            //s_addr.sin_family = AF_INET;
            //s_addr.sin_port = htons(PORT);
            //
            listenSocket.Bind(listenEndPoint);
            //if (bind(s_socket, (struct sockaddr*)&s_addr, sizeof(s_addr)) == -1){
            //    printf("Can not Bind!!!\n");
            //    return -1;
            //}
            //
            listenSocket.Listen(11);
            //if (listen(s_socket, 5) == -1)
            //{
            //    printf("Listen Fail!!!\n");
            //    return -1;
            //}
            //
            // 동기 서버
            Console.WriteLine($"[서버] {listenEndPoint.Address}:{listenEndPoint.Port} 에서 대기 중...");
            Console.WriteLine();
            while (true)
            {
                Socket clientSocket = listenSocket.Accept();
                IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine($"[서버] 클라이언트 연결됨 → {clientEndPoint.Address}:{clientEndPoint.Port}");


                byte[] buffer = new byte[BUFSIZE];

                int RecvLength = clientSocket.Receive(buffer);
                
                if (RecvLength == 9)
                {
                    string[] str = Encoding.UTF8.GetString(buffer).Split(" + ");

                    byte[] SendBuffer = new byte[BUFSIZE];
                    int result = int.Parse(str[0]) + int.Parse(str[1]);
                    
                    Console.WriteLine();

                    SendBuffer = Encoding.UTF8.GetBytes(result.ToString());
                    int SendLength = clientSocket.Send(SendBuffer, 0, SendBuffer.Length, SocketFlags.None);
                    if (SendLength < SendBuffer.Length)
                    {
                        SendLength = clientSocket.Send(SendBuffer, SendLength, SendBuffer.Length, SocketFlags.None);
                    }
                }
                else
                {
                    // 받는게 잘못됨
                    if (RecvLength <= 0)
                    {
                        // 예외 처리 필요함
                        clientSocket.Close();
                    }
                    int sendLength = clientSocket.Send(buffer);
                    // 보내다가 잘못됨
                    if (sendLength <= 0)
                    {
                        // 예외 처리 필요함
                        clientSocket.Close();

                    }
                }
                // Keep Alive Time

                clientSocket.Close();
            }
            //while (1)
            //{
            //    printf("Echo Server started...\n");
            //    len = sizeof(c_addr);
            //    c_socket = accept(s_socket, (struct sockaddr*)&c_addr, &len);
            //    printf("Connected IP : %s\n", inet_ntoa(c_addr.sin_addr));
            //    while((n = read(c_socket, rcvBuffer, sizeof(rcvBuffer))) > 0){
            //        rcvBuffer[n] = '\0';
            //        printf("%s", rcvBuffer);
            //    write(c_socket, rcvBuffer, n);
            //    }
            //    printf("client bye~~\n");
            //    close(c_socket);
            //}
            //close(s_socket);

            ///
            listenSocket.Close();
        }
    }
}
