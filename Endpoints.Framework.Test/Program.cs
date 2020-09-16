using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Endpoints.Framework.Extensions;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Endpoints.Framework.Authentication;
using System.Net;
using System.IO;
using System.Text;

namespace Endpoints.Framework.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                HttpWebRequest req = WebRequest.CreateHttp(@"https://localhost:5001/api/login?username=10105678&password=123123");
                req.ContentType = "application/json; charset=utf8";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Console.WriteLine(reader.ReadToEnd());
            }).Start();
            new Thread(() =>
            {
                HttpWebRequest req = WebRequest.CreateHttp(@"https://localhost:5001/api/login?username=1010123&password=123123");
                req.ContentType = "application/json; charset=utf8";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Console.WriteLine(reader.ReadToEnd());
            }).Start();
            new Thread(() =>
            {
                HttpWebRequest req = WebRequest.CreateHttp(@"https://localhost:5001/api/login?username=1010345&password=123123");
                req.ContentType = "application/json; charset=utf8";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Console.WriteLine(reader.ReadToEnd());
            }).Start();
            new Thread(() =>
            {
                HttpWebRequest req = WebRequest.CreateHttp(@"https://localhost:5001/api/login?username=1010456&password=123123");
                req.ContentType = "application/json; charset=utf8";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                Console.WriteLine(reader.ReadToEnd());
            }).Start();
        }
    }

}
