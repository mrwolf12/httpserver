using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static int DefaultPort;

        public HttpServer(int port)
        {
            DefaultPort = port;
        }

        public void Run()
        {
            IPAddress newIp = IPAddress.Parse("10.154.1.132");
            TcpListener serverSocket = new TcpListener(newIp, DefaultPort);
            serverSocket.Start();
            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                HttpWebServerService service = new HttpWebServerService(connectionSocket);
                Thread myThread = new Thread(service.DoIt);
                myThread.Start();
                Thread.Sleep(1);
            }
            
        }
    }
}
