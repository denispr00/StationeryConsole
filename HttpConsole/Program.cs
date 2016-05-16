using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace HttpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data : ");
            execGet(0);

            Console.WriteLine("\nselect\n0:get(filter etc),1:post,2:put:3:delete");

            int c = 0;
            c = Convert.ToInt16(Console.ReadLine());

            
            
            switch (c){
                case 1:
                    execPost();
                    break;
                case 2:
                    execPut();
                    break;
                case 3:
                    execDel();
                    break;
                default:
                    execGet(1);
                    break;
            }
            

            Console.ReadLine();
        }

        private static void execGet(int mode)
        {
            string filter = "", input, input2;
            if (mode ==0)
                goto jump;

            
            Console.WriteLine("\napply filter\n1 for (name or color), 2 for (name or color) and stock, 3 for name and colour");
            int c=Convert.ToInt16(Console.ReadLine());

            switch(c)
            {
                case 1:
                    Console.WriteLine("input name or colour");
                    input = Console.ReadLine();
                    filter="?name="+input;
                    break;
                case 2:
                    Console.WriteLine("input name or colour");
                    input = Console.ReadLine();
                    Console.WriteLine("input stock");
                    input2 = Console.ReadLine();
                    filter = "?name="+input+"&stock="+input2;
                    break;
                case 3:
                    Console.WriteLine("input name");
                    input = Console.ReadLine();
                    Console.WriteLine("input colour");
                    input2 = Console.ReadLine();
                    filter = "?name=" + input + "&color=" + input2;
                    break;
                default:
                    filter = "";
                    break;
            }

            jump:

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create
                (string.Format("http://localhost:65200/api/stationery/{0}", filter));
            
            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();


            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            Console.WriteLine(_Answer.ReadToEnd());


            if(mode!=0)
                execGet(1);
        } 

        private static void execPost()
        {
            String name, colour,filter;
            int stock;

            Console.WriteLine("\ninput name");
            name = Console.ReadLine();
            Console.WriteLine("\ninput colour");
            colour = Console.ReadLine();
            Console.WriteLine("input stock");
            stock = Convert.ToInt16(Console.ReadLine());
            filter = "name=" + name + "&colour="+colour+"&stock=" + stock;

            
            byte[] buffer = Encoding.ASCII.GetBytes(filter);

            HttpWebRequest WebReq =
            (HttpWebRequest)WebRequest.Create("http://localhost:65200/api/stationery/");
            

            WebReq.Method = "POST";
            

            WebReq.ContentType = "application/x-www-form-urlencoded";
            

            WebReq.ContentLength = buffer.Length;
            

            Stream PostData = WebReq.GetRequestStream();
            

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            

            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            Console.WriteLine(_Answer.ReadToEnd());

            
        }

        private static void execPut()
        {
            Console.WriteLine("\ninput name of stationery you want to modify the stock");
            String name,filter;
            int stock;

            name = Console.ReadLine();
            Console.WriteLine("input stock");
            stock = Convert.ToInt16(Console.ReadLine());
            filter = "?name=" + name +"&stock=" + stock;

            

            byte[] buffer = Encoding.ASCII.GetBytes(filter);
            

            HttpWebRequest WebReq =
            (HttpWebRequest)WebRequest.Create("http://localhost:65200/api/stationery/"+filter);
            

            WebReq.Method = "PUT";
            

            WebReq.ContentType = "application/x-www-form-urlencoded";
            

            WebReq.ContentLength = buffer.Length;
            

            Stream PostData = WebReq.GetRequestStream();
            

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);


            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            Console.WriteLine(_Answer.ReadToEnd());
            
        
        }

        private static void execDel()
        {
            Console.WriteLine("\ninput name of stationery you want to delete");
            String name,filter;

            name = Console.ReadLine();
            filter = "?name=" + name;

            

            byte[] buffer = Encoding.ASCII.GetBytes(filter);
            

            HttpWebRequest WebReq =
            (HttpWebRequest)WebRequest.Create("http://localhost:65200/api/stationery/" + filter);
            

            WebReq.Method = "DELETE";
            

            WebReq.ContentType = "application/x-www-form-urlencoded";
            

            WebReq.ContentLength = buffer.Length;
            

            Stream PostData = WebReq.GetRequestStream();
            

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            

            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            Console.WriteLine(_Answer.ReadToEnd());

        }
    }
}
