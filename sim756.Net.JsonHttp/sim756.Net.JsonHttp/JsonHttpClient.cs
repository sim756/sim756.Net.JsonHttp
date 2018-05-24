using System;
using System.Net;
using Newtonsoft.Json;
using sim756.Net.JsonHttp.Exceptions;

namespace sim756.Net.JsonHttp
{
    public class JsonHttpClient<T>
    {
        public string Url { get; set; }

        public JsonHttpClient()
        {

        }

        public JsonHttpClient(string url)
        {
            this.Url = url;
        }


        public static void Post()
        {

        }

        public T GetDeserializedObject()
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new UrlEmptyException();
            }

            return JsonConvert.DeserializeObject<T>(new WebClient().DownloadString(Url));
            //return default(T);
        }
    }
}