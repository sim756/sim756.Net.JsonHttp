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

            //1 Examples - JsonHttpClient<T>
            //1.1
            example = new JsonHttpClient<Example>("http://www.example.com").Deserialize();

            //2 Examples - JsonHttpClient
            //2.1            
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com");

            //2.2
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com", new System.Net.WebClient()
            {
                BaseAddress = "http://www.example.com",
                Encoding = Encoding.Unicode,
                Credentials = new Credential()
            });

            //2.3
            example = JsonHttpClient.DeserializeString<Example>("....JSON....");

            //2.4
            json = JsonHttpClient.Get("http://www.example.com");

            //2.5
            json = JsonHttpClient.Get("http://www.example.com", new System.Net.WebClient()
            {
                BaseAddress = "http://www.example.com",
                Encoding = Encoding.Unicode,
                Credentials = new Credential()
            });

            //2.6
            json = JsonHttpClient.Serialize(new Example()
            {
                Uid = 756,
                Property1 = "sim756",
                Property2 = true
            });            
        }
    }
}