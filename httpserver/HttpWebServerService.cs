using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace httpserver
{
    public class HttpWebServerService
    {
        /// <summary>
        /// parameter til forbindelse og fildestination.
        /// </summary>
        private readonly TcpClient _connectionSocket;
        private static readonly string RootCatalog = @"C:\Users\Morten\Documents\Visual Studio 2013\Projects\WebServer2014\httpserver\opgave";

        /// <summary>
        /// Constructor for forbindelsen.
        /// </summary>
        /// <param name="connectionSocket"></param>
        public HttpWebServerService(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }

        /// <summary>
        /// opretter læse og skive enheder for applikationen
        /// og opretter et byet arry at forspørgelsen
        /// udskiver forspørgelsen.
        /// </summary>
        public void DoIt()
        {
            Stream ns = _connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            try
            {
                
                string req = sr.ReadLine();
                Console.WriteLine(req);
                if (req != null)
                {
                    string[] word = req.Split(' ');

                    if (word[2] != "HTTP/1.0")
                    {
                        throw new IOException();
                    }
                    else if ((word[1] == "/"))
                    {
                        throw new NullReferenceException();
                    }
                    else
                    {
                        GetValue(word, sw, ns);
                    }
                }
            }
            catch (NullReferenceException)
            {
                byte[] badRequest = Encoding.UTF8.GetBytes("HTTP Error 400 Bad request\r\n");
                ExceptionHandling(ns, badRequest, sw, sr);
            }
            catch (FileNotFoundException)
            {
                byte[] badRequest = Encoding.UTF8.GetBytes("HTTP/1.0 404 File not found\r\n");
                ExceptionHandling(ns, badRequest, sw, sr);
            }
            catch (IOException)
            {
                byte[] badRequest = Encoding.UTF8.GetBytes("HTTP/1.0 400 Illegal request\r\n");
                ExceptionHandling(ns, badRequest, sw, sr);
            }
            finally
            {
                _connectionSocket.Close();
            }
        }

        /// <summary>
        /// Håndtere undtagelser der forkommer.
        /// </summary>
        /// <param name="ns"></param>
        /// <param name="badRequest"></param>
        /// <param name="sw"></param>
        /// <param name="sr"></param>
        private static void ExceptionHandling(Stream ns, byte[] badRequest, StreamWriter sw, StreamReader sr)
        {
            ns.Write(badRequest, 0, badRequest.Length);
            sw.Write("\r\n");
            sr.ReadLine();
        }


        /// <summary>
        /// kontrolere om filen findes 
        /// og håndtere undtagelsen hvis ikke den findes.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="sw"></param>
        /// <param name="ns"></param>
        private static void GetValue(string[] word, StreamWriter sw, Stream ns)
        {
            string filname = "/" + word[1];
            
            if (!File.Exists(RootCatalog + filname))
            {
                throw new FileNotFoundException();
            }

            using (FileStream source = File.Open(RootCatalog + filname, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                sw.Write("HTTP/1.0 200 OK\r\n");
                sw.Write("\r\n");
            }
        }
    }
}
