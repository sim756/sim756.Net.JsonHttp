using sim756.Net.JsonHttp;
using System;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("sim756.Net.JsonHttp.JsonHttpClient");
            Console.WriteLine("");

            string retVal = string.Empty;

            retVal = JsonHttpClient.Deserialize<string>("http://www.example.com");

            retVal = JsonHttpClient.Deserialize<string>("http://www.example.com",new System.Net.WebClient()
            {

            });
        }
    }
}