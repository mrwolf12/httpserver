using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace httpserver
{
    class HttpWebServerService
    {
        private readonly TcpClient _connectionSocket;

        public HttpWebServerService(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }

        public void DoIt()
        {
            Stream ns = _connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

        }
    }
}
