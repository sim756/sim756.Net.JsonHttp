using sim756.Net.JsonHttp;
using System;
using System.Text;
using System.Net;
using System.Net.Http;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Example example;
            string json;            
            HttpClient httpClient = new HttpClient()
            {

            };

            //1 Examples - JsonHttpClient<T>
            //1.1
            example = new JsonHttpClient<Example>("http://www.example.com").Deserialize();

            //1.2
            example = new JsonHttpClient<Example>("http://www.example.com", new HttpClient()).Deserialize();

            //1.3
            example = new JsonHttpClient<Example>("http://www.example.com", new HttpClient()
            {

            }).Deserialize();

            //1.4
            example = new JsonHttpClient<Example>().Deserialize("http://www.example.com");

            //1.5
            example = new JsonHttpClient<Example>().Deserialize("http://www.example.com", httpClient);

            //1.6
            example = new JsonHttpClient<Example>()
            {
                Url = "http://www.example.com",
                HttpClient = httpClient
            }.Deserialize();

            //1.7
            JsonHttpClient<Example> jsonHttpClient = new JsonHttpClient<Example>("http://www.example.com");
            jsonHttpClient.DeserializeInside();

            int uid = jsonHttpClient.Object.Uid;
            string property1 = jsonHttpClient.Object.Property1;
            bool property2 = jsonHttpClient.Object.Property2;


            //2 Examples - JsonHttpClient
            //2.1            
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com");

            //2.2
            example = JsonHttpClient.Deserialize<Example>("http://www.example.com", webClient);

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