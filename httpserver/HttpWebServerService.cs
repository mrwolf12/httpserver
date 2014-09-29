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
        private static readonly string RootCatalog = @"C:\Users\Morten\Documents\Visual Studio 2013\Projects\WebServer2014\httpserver\opgave";

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

                    string req = sr.ReadLine();

                    string[] word = req.Split(' ');

                    if ((word[1] == "/"))
                    {
                        throw new NullReferenceException("1");
                    }
                    else
                    {
                        sw.Write("You have requested: " + word[1]);
                        sw.Write("\r\n");

                        string filname = word[1] + ".txt";
                        if (!File.Exists(RootCatalog + filname))
                        {
                            throw new FileNotFoundException();
                        }

                        using (FileStream source = File.Open(RootCatalog + filname, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            sw.Write("HTTP/1.0 200 OK \r\n");
                            sw.Write("\r\n");
                            source.CopyTo(ns);
                            source.Close();
                        }
                    }
                    

                }
                catch (NullReferenceException)
                {
                    byte[] badRequest = Encoding.UTF8.GetBytes("HTTP Error 400 Bad request \r\n");
                    ns.Write(badRequest, 0, badRequest.Length);
                    sw.Write("\r\n");
                    sr.ReadLine();
                }
                catch (FileNotFoundException)
                {
                    byte[] notFound = Encoding.UTF8.GetBytes("HTTP/1.0 404 File not found \r\n");
                    ns.Write(notFound, 0, notFound.Length);
                    sw.Write("\r\n");
                    sr.ReadLine();
                }
                catch (IOException)
                {
                    byte[] error1 = Encoding.UTF8.GetBytes("HTTP/1.0 400 BAD REQUEST \r\n");
                    ns.Write(error1, 0, error1.Length);
                    sw.Write("\r\n");
                    sr.ReadLine();
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
