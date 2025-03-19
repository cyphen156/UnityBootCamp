using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Client
{
    internal class Program
    {
        class Message
        {

        }
        static int Main(string[] args)
        {
            const int BUFSIZE = 1024;
            const int LISTENPORT = 3000;
            const string END = "END\0";
            // 제이슨 파일 읽어서 버퍼에 집어넣기 먼저하기
            string basePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            string path = basePath + "\\clientMessage.json";
            StreamReader sr = new StreamReader(path);
            if (sr == null)
            {
                Console.WriteLine("파일 없어요");
            }
            string message = File.ReadAllText(path);
            
            sr.Close();
            // 나중에 하기
            // 제이슨 오브젝트로 보내보기
            //JObject jsonMessage = JObject.Parse(message);

            // 서버 연결
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), LISTENPORT);
            serverSocket.Connect(serverEndPoint);

            // 주기 시작
            byte[] sendBuffer = new byte[BUFSIZE];
            sendBuffer = Encoding.UTF8.GetBytes(message);
            int SendLength = serverSocket.Send(sendBuffer, 0, sendBuffer.Length, SocketFlags.None);

            // 받기 시작
            while (true)
            {
                byte[] recvBuffer = new byte[BUFSIZE * 10];
                int recvLength = serverSocket.Receive(recvBuffer);
                string getMessage = Encoding.UTF8.GetString(recvBuffer);
                // 맨 처음 뭐가 오나?
                Console.WriteLine(getMessage);
                if (getMessage.CompareTo(END) == 0)
                {
                    // 통신중 모든 파일 전송이 끝남을 의미
                    break;
                }
                else
                {
                    // 혹시 모를 이전 버퍼 데이터 더미 치우기
                    Array.Clear(recvBuffer, 0, recvBuffer.Length);

                    int offset = 0;
                    int length = 4;
                    
                    // 무조건 길이는 4바이트 씩 받을거임
                    // 파일 사이즈 받앗음
                    recvLength = serverSocket.Receive(recvBuffer, offset, length, SocketFlags.None);
                    //string data = Encoding.UTF8.GetString(recvBuffer, offset, length);
                    int fileSize = BitConverter.ToInt32(recvBuffer, offset);
                    offset += recvLength;
                    Console.WriteLine(fileSize);

                    // 파일 이름 길이 받아오기
                    recvLength = serverSocket.Receive(recvBuffer, offset, recvLength, SocketFlags.None);
                    //data = Encoding.UTF8.GetString(recvBuffer, offset, recvLength);
                    int fileNameLength = BitConverter.ToInt32(recvBuffer, offset);
                    offset += recvLength;
                    //offset;
                    Console.WriteLine(fileNameLength);

                    // 파일 이름 받아오기
                    recvLength = serverSocket.Receive(recvBuffer, offset, fileNameLength, SocketFlags.None);
                    string fileName = Encoding.UTF8.GetString(recvBuffer, offset, recvLength);
                    offset += recvLength;
                    Console.WriteLine(fileName);
                    Console.WriteLine();
                    Array.Clear(recvBuffer, 0, recvBuffer.Length);

                    byte[] fileDataBuffer = new byte[BUFSIZE];
                    int totalReceived = 0;
                    string savePath = basePath + fileName;

                    using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                    {
                        while (totalReceived < fileSize)
                        {
                            int bytesRead = serverSocket.Receive(fileDataBuffer, 0, Math.Min(BUFSIZE, fileSize - totalReceived), SocketFlags.None);
                            fs.Write(fileDataBuffer, 0, bytesRead);
                            totalReceived += bytesRead;
                        }
                    }

                    Console.WriteLine($"파일 저장 완료: {savePath}");
                    Console.WriteLine();
                }

                // 서버로부터 받은 파일 저장하기
                //string savePath = basePath + "Image.Webp";
                //StreamWriter sw = new StreamWriter(savePath);

            }
            // 에코 통신 다했으면 소켓반납해라
            serverSocket.Close();

            // 디버그 다운되지 마라
            return 0;
        }
    }
}
