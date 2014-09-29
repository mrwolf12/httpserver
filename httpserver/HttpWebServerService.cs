using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace httpserver
{
    public class HttpWebServerService
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
            while (_connectionSocket.Connected)
            { 
                try
                {
                    byte[] request = Encoding.UTF8.GetBytes("GET / HTTP/1.0 \r\n \r\n");
                    ns.Write(request, 0, request.Length);
                    sw.Write("\r\n");
                    sw.Write("hallo \r\n");
                    sr.ReadLine();
                    ns.Flush();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    _connectionSocket.Close();
                }
            }
        }
    }
}
