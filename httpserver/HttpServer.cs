using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace httpserver
{
    public class HttpServer
    {
        /// <summary>
        /// server classen som sætter port og ip adresse
        /// samt acceptere indkommen clienter 
        /// </summary>
        public static int DefaultPort = 8888;

        public void Run()
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress local = ipHost.AddressList[0];
            TcpListener serverSocket = new TcpListener(local, DefaultPort);
            serverSocket.Start();
            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                HttpWebServerService service = new HttpWebServerService(connectionSocket);
                Thread myThread = new Thread(service.DoIt);
                myThread.Start();
                //Thread.Sleep(1);
            }
            
        }
    }
}
