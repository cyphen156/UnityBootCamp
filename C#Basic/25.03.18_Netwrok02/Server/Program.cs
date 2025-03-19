using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server
{
    internal class Program
    {
        /// <summary>
        /// Server Program
        /// </summary>
        /// <param name="Server"></param>
        static void Main(string[] args)
        {
            const int BUFSIZE = 1024;
            const int LISTENPORT = 3000;
            const string MESSAGE_OK = "{\r\n    \"message\" :   \"안녕하세요\"\r\n}\r\n";
            string basePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string savePath;

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), LISTENPORT);
            listenSocket.Bind(iPEndPoint);

            listenSocket.Listen(10);

            bool isRunning = true;
            bool isDone = false;

            while (isRunning)
            {
                Socket clientSocket = listenSocket.Accept();
                IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                if (clientEndPoint == null)
                {
                    Console.WriteLine("연결 안됨");
                    clientSocket.Close();
                }
                else 
                {
                    Console.WriteLine($"연결된 클라이언트 → {clientEndPoint.Address}:{clientEndPoint.Port}");
                }

                // 받기 시작
                byte[] recvBuffer = new byte[BUFSIZE];
                int recvLength = clientSocket.Receive(recvBuffer);
                string getMessage = Encoding.UTF8.GetString(recvBuffer, 0, recvLength);
                string echoMessage;
                if (getMessage.CompareTo(MESSAGE_OK) == 0)
                {
                    Console.WriteLine(getMessage);
                    // 받은 메세지 제이슨파일로 저장하기
                    savePath = basePath + "\\ServerGetMessage.json";
                    StreamWriter sw = new StreamWriter(savePath);
                    sw.WriteLine(getMessage);

                    sw.Close();
                    // 주기 시작 :: 텍스트
                    echoMessage = "{ \"message\"    :   \"반가워요\"}";
                    byte[] sendBuffer = new byte[BUFSIZE];
                    sendBuffer = Encoding.UTF8.GetBytes(echoMessage);
                    int sendLength = clientSocket.Send(sendBuffer);
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);

                    // 이제 그림 줄거야
                    // 그 전에 파일읽기
                    string fileName = @"\1.webp";
                    string filePath = basePath + fileName;
                    if (File.Exists(filePath) && !isDone)
                    {
                        
                        // 파일 크기 먼저 보내고
                        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        int fileSize = (int)fs.Length;
                        byte[] filelengthBuffer = new byte[4];
                        filelengthBuffer = BitConverter.GetBytes(fileSize);
                        sendLength = clientSocket.Send(filelengthBuffer, 0, filelengthBuffer.Length, SocketFlags.None);
                        sendLength = 0;

                        // 파일 이름 길이 보내고
                        int fileNameLength = (int)fileName.Length;
                        byte[] fileNamelengthBuffer = new byte[4];
                        fileNamelengthBuffer = BitConverter.GetBytes(fileNameLength);
                        sendLength = clientSocket.Send(fileNamelengthBuffer, 0, fileNamelengthBuffer.Length, SocketFlags.None);
                        sendLength = 0;

                        // 파일 이름 보내고 
                        byte[] filenameBuffer = Encoding.UTF8.GetBytes(fileName);
                        sendLength = clientSocket.Send(filenameBuffer, 0, filenameBuffer.Length, SocketFlags.None);
                        sendLength = 0;

                        // 파일 내용 보내기
                        byte[] fileDataBuffer = new byte[BUFSIZE];
                        int bytesRead;
                        while ((bytesRead = fs.Read(fileDataBuffer, 0, fileDataBuffer.Length)) > 0)
                        {
                            clientSocket.Send(fileDataBuffer, bytesRead, SocketFlags.None);
                        }
                        Console.WriteLine($"파일 {filePath} 전송 완료");

                        isDone = true;
                        //Console.WriteLine();
                        fs.Close();
                    }
                    // 이제 다줬으니까 종료시퀀스 보내줌
                    echoMessage = "";
                    byte[] endBuffer = new byte[echoMessage.Length];
                    endBuffer = Encoding.UTF8.GetBytes(echoMessage);
                    echoMessage = "END\0";
                    endBuffer = Encoding.UTF8.GetBytes(echoMessage);
                    int Length = clientSocket.Send(endBuffer);
                }
                else 
                {
                    Console.WriteLine(1 + getMessage);
                }
                
                // 에코 통신 다했으면 소켓반납해라
                clientSocket.Close();
            }
            // 서버끈다
            listenSocket.Close();
        }
    }
}
