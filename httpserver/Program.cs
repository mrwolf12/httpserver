
namespace httpserver
{
    public class Program
    {
        /// <summary>
        /// opstart af programmet.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var newRun = new HttpServer();
            newRun.Run();
        }
    }
}
