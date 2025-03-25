using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Packet
{
    public string code;
    public string id;
}


public class LoginPacket : Packet
{

    public string password;
}

[Serializable]
public class SignupPacket : Packet
{

    public string password;
    public string name;
    public string email;
}

public class ChatPacket : Packet
{
    public string message;
}

public class NetworkManager : MonoBehaviour
{
    private Socket serverSocket;
    private IPEndPoint serverEndPoint;
    private Thread recvThread;

    public TMP_InputField newIdUI;
    public TMP_InputField newPasswordUI;
    public TMP_InputField nameUI;
    public TMP_InputField emailUI;

    public TMP_InputField idUI;
    public TMP_InputField passwordUI;

    public Queue<Packet> data;

    public TMP_InputField ChatInput;
    public TMP_Text ServerMessage;

    public Queue<string> serverMessageBuffer = new Queue<string>();
    void Start()
    {
        ConnectedToServer();
    }

    void RecvPacket()
    {
        while (true)
        {
            byte[] lengthBuffer = new byte[2];

            int RecvLength = serverSocket.Receive(lengthBuffer, 2, SocketFlags.None);
            ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
            length = (ushort)IPAddress.NetworkToHostOrder((short)length);
            byte[] recvBuffer = new byte[4096];
            RecvLength = serverSocket.Receive(recvBuffer, length, SocketFlags.None);

            string jsonString = Encoding.UTF8.GetString(recvBuffer);

            serverMessageBuffer.Enqueue(jsonString);
            Debug.Log(jsonString);
            //Parsing
            //Thread.Sleep(10);

            //Do Not, Never
            //GameObject.Find("NetworkManager").transform.Translate(Vector3.up);
        }
    }

    void ConnectedToServer()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000);
        serverSocket.Connect(serverEndPoint);
        recvThread = new Thread(new ThreadStart(RecvPacket));
        recvThread.IsBackground = true;
        recvThread.Start();
    }

    void SendPacket(string message)
    {
        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

        byte[] headerBuffer = BitConverter.GetBytes(length);

        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

        int SendLength = serverSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
    }

    public void OnLogin()
    {
        LoginPacket packet = new LoginPacket();
        packet.code = "Login";
        packet.id = idUI.text;
        packet.password = passwordUI.text;

        SendPacket(JsonUtility.ToJson(packet));
    }

    public void OnSignup()
    {
        SignupPacket packet = new SignupPacket();
        packet.code = "Signup";
        packet.id = newIdUI.text;
        packet.password = newPasswordUI.text;
        packet.email = emailUI.text;
        packet.name = nameUI.text;

        SendPacket(JsonUtility.ToJson(packet));
    }
    public void OnSendMessage()
    {
        ChatPacket packet = new ChatPacket();
        packet.code = "Chat";
        packet.id = "Client";
        packet.message = ChatInput.text;
        SendPacket(JsonUtility.ToJson(packet));

    }

    private void Update()
    {
        if (serverMessageBuffer.Count > 0)
        {
            string serverMessage;
            string recvMessage = serverMessageBuffer.Dequeue();
            Packet basePacket = JsonUtility.FromJson<Packet>(recvMessage);
            serverMessage = basePacket.id.ToString();
            if (basePacket.code == "Chat")
            {
                ChatPacket chatPacket = JsonUtility.FromJson<ChatPacket>(recvMessage);
                serverMessage = serverMessage + " : " + chatPacket.message.ToString();
            }
            ServerMessage.text = serverMessage;
        }
    }

    private void OnApplicationQuit()
    {
        if (recvThread != null)
        {
            recvThread.Abort();
        }

        if(serverSocket != null)
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();

        }
    }

}
