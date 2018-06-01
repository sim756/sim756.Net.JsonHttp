using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using sim756.Net.JsonHttp.Exceptions;

namespace sim756.Net.JsonHttp
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonHttpClient
    {
        /// <summary>
        /// Downloads JSON from specified URL using WebClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="webClient">Optional. Customized WebClient.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static string Get(string url, WebClient webClient = null)
        {
            if (webClient == null)
            {
                return new WebClient().DownloadString(url);
            }
            return webClient.DownloadString(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="webClient">Optional. Customized WebClient.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string url, WebClient webClient = null)
        {
            return JsonConvert.DeserializeObject<T>((webClient ?? new WebClient()).DownloadString(url));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="webClient"></param>
        public static void Post(string json, WebClient webClient = null)
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
        public T Object { get; set; }

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
            return JsonConvert.DeserializeObject<T>((webClient ?? new WebClient()).DownloadString(IsUrlNull(url)));
        }

        /// <summary>
        /// Deserializes JSON to specified type T
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Deserialized object of type T.</returns>
        public T Deserialize(string json)
        {            
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="webClient"></param>
        public void Post(T objectToPost, WebClient webClient = null)
        {
            throw new NotImplementedException();
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