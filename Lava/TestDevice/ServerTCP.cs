using ConnectedDevice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public static class ServerTCP 
{
    public static event Action<byte[]> DataLoadEvent;

    public static void StartRead()
    {
        Thread server = new Thread(ThreadServer);
        server.Start();
    }

    public static void ThreadServer()
    {
        string ip = "127.0.0.1";
        int port = 8080;

        var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        tcpSocket.Bind(tcpEndPoint);
        tcpSocket.Listen(5);

        while (true)
        {
            try
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[66];
                var size = 0;

                do
                {
                    size = listener.Receive(buffer);
                }
                while (listener.Available > 0);

                DataLoadEvent?.Invoke(buffer);

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }

    public static void SendData(byte[] data)
    {
        string ip = "127.0.0.1";
        int port = 8081;

        var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        tcpSocket.Connect(tcpEndPoint);
        tcpSocket.Send(data);


        tcpSocket.Shutdown(SocketShutdown.Both);
        tcpSocket.Close();
    }
}
