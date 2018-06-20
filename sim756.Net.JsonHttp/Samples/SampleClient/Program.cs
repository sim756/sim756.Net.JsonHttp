using System;
using sim756.Net.JsonHttp;
using SampleModels;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JsonHttpClient SampleClient");
            Console.WriteLine("");

            //Example 1
            {
                Console.WriteLine("Example 1");

                Console.WriteLine(new JsonHttpClient<Model1>("http://localhost:5000/api/values").Deserialize().Property1);
            }

            Console.WriteLine("");

            //Example 2
            {
                Console.WriteLine("Example 2");

                JsonHttpClient<Model1> jsonHttpClient = new JsonHttpClient<Model1>("http://localhost:5000/api/values");
                jsonHttpClient.DeserializeInside();

                Console.WriteLine(jsonHttpClient.Object.Property1);
            }

            Console.WriteLine("");

            //Example 3
            {
                Console.WriteLine("Example 3");

                Model1 model1 = JsonHttpClient.Deserialize<Model1>("http://localhost:5000/api/values");

                Console.WriteLine(model1.Property1);
            }

            Console.WriteLine("");
            Console.Write("press any key to continue...");
            Console.ReadKey();
        }
    }
}