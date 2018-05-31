using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using sim756.Net.JsonHttp.Exceptions;

namespace sim756.Net.JsonHttp
{
    public class JsonHttpClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="webClient"></param>
        /// <returns></returns>
        public static string Get(string url, WebClient webClient = null)
        {
            return webClient.DownloadString(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="webClient"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string url, WebClient webClient = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonHttpClient<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WebClient WebClient { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public JsonHttpClient()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Url Property.</param>
        public JsonHttpClient(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public static void Post(string json)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string IsUrlNull(string url)
        {
            if (url == null && string.IsNullOrEmpty(Url))
            {
                throw new EmptyUrlException();
            }

            if (url == null && string.IsNullOrEmpty(Url) == false)
            {
                url = this.Url;
            }

            if (url != null && string.IsNullOrEmpty(Url) == true)
            {
                this.Url = url;
            }

            return url;
        }

        /// <summary>
        /// Gets JSON from the Url Property (otherwise set on url parameter) and returns the deserialized object of type T.
        /// </summary>
        /// <param name="url">Optional, assign or left "null" to use Url property.</param>
        /// <param name="webClient">Customized WebClient or left "null" to use default.</param>
        /// <returns>Deserialized object of type T.</returns>
        public T Deserialize(string url = null, WebClient webClient = null)
        {
            url = IsUrlNull(url);

            if (webClient == null)
            {
                return JsonConvert.DeserializeObject<T>(new WebClient().DownloadString(Url));
            }

            return JsonConvert.DeserializeObject<T>(webClient.DownloadString(Url));

            //return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);            
        }

        /// <summary>
        /// [Deprecated] Use Deserialize() instead of GetDeserializedObject.
        /// </summary>
        /// <returns></returns>
        public static T GetDeserializedObject()
        {
            throw new DeprecatedException("Use Deserialize() instead of GetDeserializedObject.");
        }
    }
}