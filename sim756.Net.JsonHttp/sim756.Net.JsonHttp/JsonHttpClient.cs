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
        /// Downloads JSON/Text from specified URL using WebClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="webClient">Optional. Customized WebClient.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static string Get(string url, WebClient webClient = null)
        {
            return (webClient ?? new WebClient()).DownloadString(url);
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
        /// Deserializes JSON into specified type T.
        /// </summary>
        /// <typeparam name="T">Type to deserialize the object into.</typeparam>
        /// <param name="json">JSON.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static T DeserializeString<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Serializes an object into JSON string.
        /// </summary>
        /// <param name="value">Object to be serialized.</param>
        /// <returns>JSON.</returns>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="webClient"></param>
        public static void Post(string json, WebClient webClient)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="url"></param>
        public static void Post(string json, string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        public void Post<T>(T objectToPost, string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="webClient"></param>
        public void Post<T>(T objectToPost, WebClient webClient)
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
        /// <param name="webClient"></param>
        public JsonHttpClient(string url, WebClient webClient)
        {
            this.Url = url;
            this.WebClient = webClient;
        }

        /// <summary>
        /// [For internal usage of the class only.]
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns>URL.</returns>        
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
        /// <param name="url">URL to download JSON from. Optional when Url Property is set, assign or left "null" to use Url property.</param>
        /// <param name="webClient">Customized WebClient or left "null" to use default.</param>
        /// <param name="keepWebClient"></param>
        /// <returns>Deserialized object of type T.</returns>
        public  T Deserialize(string url = null, WebClient webClient = null, bool keepWebClient = false)
        {
            IsKeepWebClient(ref webClient, keepWebClient);
            return JsonConvert.DeserializeObject<T>((webClient ?? new WebClient()).DownloadString(IsUrlNull(url)));
        }

        /// <summary>
        /// [For internal usage of the class only.]
        /// </summary>
        /// <param name="webClient">Customized WebClient.</param>
        /// <param name="keepWebClient">Whether to assign webClient into WebClient Property.</param>
        private void IsKeepWebClient(ref WebClient webClient, bool keepWebClient)
        {
            if (keepWebClient == true)
            {
                this.WebClient = webClient;
            }
        }

        /// <summary>
        /// Downloads JSON from Url, deserializes it into the type of T and assigns it into the Object property.
        /// </summary>
        /// <param name="url">URL to download JSON from. Optional when Url Property is set, assign or left "null" to use Url property.</param>
        /// <param name="webClient">Customized WebClient or left "null" to use default.</param>
        /// <param name="keepWebClient"></param>
        public void DeserializeInside(string url = null, WebClient webClient = null, bool keepWebClient = false)
        {
            IsKeepWebClient(ref webClient, keepWebClient);
            Object = JsonConvert.DeserializeObject<T>(((webClient ?? this.WebClient) ?? new WebClient()).DownloadString(IsUrlNull(url)));
        }

        /// <summary>
        /// Deserializes JSON string into the type of T and assigns into the Object property.
        /// </summary>
        /// <param name="json">JSON to deserialize.</param>
        public void DeserializeStringInside(string json)
        {
            Object = JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Post()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void Post(string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        public void Post(T objectToPost, string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="webClient"></param>
        /// <param name="keepWebClient"></param>
        public void Post(T objectToPost, WebClient webClient = null, bool keepWebClient = false)
        {
            IsKeepWebClient(ref webClient, keepWebClient);
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