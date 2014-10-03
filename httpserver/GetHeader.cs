using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class GetHeader
    {
        public static string ResponsHeader(string type)
        {
            string headerType;
            string contentype;
            if (type == "html" || type == "htm")
            {
                headerType = "text/html";
                contentype = "Content-Type : " + headerType;
            }
            else if (type == "doc")
            {
                headerType = "application/msword";
                contentype = "Content-Type : " + headerType;
            }
            else if (type == "gif")
            {
                headerType = "image/gif";
                contentype = "Content-Type : " + headerType;
            }
            else if (type == "jpg")
            {
                headerType = "image/jpeg";
                contentype = "Content-Type : " + headerType;
            }
            else
            {
                contentype = "Content-Type : missing";
            }
            return contentype;
        }
    }
}
