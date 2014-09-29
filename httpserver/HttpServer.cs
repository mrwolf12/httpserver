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
        public string Ip;

        public HttpServer(string ip, int port)
        {
            DefaultPort = port;
            Ip = ip;
        }

        public void run()
        {
            IPAddress newIp = IPAddress.Parse(Ip);
            TcpListener serverSocket = new TcpListener(newIp, DefaultPort);
            serverSocket.Start();
            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated now");
                HttpWebServerService service = new HttpWebServerService(connectionSocket);
                //Thread myThread = new Thread(new ThreadStart(service.DoIt));
                //myThread.Start();
                //Thread.Sleep(1);
                //service.DoIt();
                //Task.Factory.StartNew(service.doIt);
                // or use delegates Task.Factory.StartNew() => service.DoIt();
            }
        }
    }
}
