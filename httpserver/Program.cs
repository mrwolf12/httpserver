using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpServer newRun = new HttpServer(8888);
            newRun.run();
        }
    }
}
