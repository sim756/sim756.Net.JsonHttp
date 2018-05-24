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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets JSON string from the url.
        /// </summary>
        /// <param name="url">Optional, assign or left "null" to use Url property.</param>
        /// <returns>JSON in string.</returns>
        public string Get(string url = null)
        {
            return new WebClient().DownloadString(url);
        }

        /// <summary>
        /// Gets JSON from the Url Property (otherwise set on url parameter) and returns the deserialized object of type T.
        /// </summary>
        /// <param name="url">Optional, assign or left "null" to use Url property.</param>
        /// <returns>Deserialized object of type T.</returns>

        public T GetDeserializedObject(string url = null)
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new UrlEmptyException();
            }

            return JsonConvert.DeserializeObject<T>(new WebClient().DownloadString(Url));
            //return default(T);
        }

        public static TDeserialize GetDeserializedObject<TDeserialize>(string url)
        {

            return default(TDeserialize);
        }
    }
}