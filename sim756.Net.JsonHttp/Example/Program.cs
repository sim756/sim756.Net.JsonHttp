using sim756.Net.JsonHttp;
using System;
using System.Text;
using System.Net;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("sim756.Net.JsonHttp.JsonHttpClient");
            Console.WriteLine("");

            Example example;
            string json;

            //1
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com");

            //2
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com", new System.Net.WebClient()
            {
                BaseAddress = "http://www.example.com",
                Encoding = Encoding.Unicode,
                Credentials = new Credential()
            });

            //3
            example = JsonHttpClient.DeserializeString<Example>("....JSON....");


        }
    }
}