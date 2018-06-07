using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using sim756.Net.JsonHttp.Exceptions;

namespace sim756.Net.JsonHttp
{
    /// <summary>
    /// A simplified Generic method to GET or POST JSON using HTTP, and get it deserialized or serialized into the Object of specified type.
    /// </summary>
    public class JsonHttpClient
    {
        /// <summary>
        /// Downloads JSON from specified URL.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static string Get(string url)
        {
            return (new WebClient()).DownloadString(url);
        }

        /// <summary>
        /// Downloads JSON from specified URL using WebClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="webClient">Optional. Customized WebClient.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static string Get(string url, WebClient webClient)
        {
            return (webClient ?? new WebClient()).DownloadString(url);
        }

        /// <summary>
        /// Downloads JSON from the URL using default WebClient and returns the object into type T.
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static T Deserialize<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(new WebClient().DownloadString(url));
        }

        /// <summary>
        /// Downloads JSON from the URL. 
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="webClient">Optional. Customized WebClient.</param>
        /// <returns>Deserialized object into type T.</returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL.</param>
        /// <param name="webClient">WebClient.</param>
        public void Post<T>(T objectToPost, string url, WebClient webClient)
        {
            if (webClient == null)
            {
                throw new WebClientException();
            }
            webClient.UploadString(url, "POST", JsonConvert.SerializeObject(objectToPost, Formatting.Indented));
        }

        /// <summary>
        /// Posts JSON to the URL using the specified WebClient.
        /// </summary>
        /// <param name="json">JSON.</param>
        /// <param name="url">URL.</param>
        /// <param name="webClient">WebClient.</param>
        public static void Post(string json, string url, WebClient webClient)
        {
            if (webClient == null)
            {
                throw new WebClientException();
            }
            webClient.UploadString(url, "POST", json);
        }

        /// <summary>
        /// Serializes the object and posts it to the URL specified.
        /// </summary>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL.</param>
        public void Post<T>(T objectToPost, string url)
        {
            new WebClient().UploadString(url, "POST", JsonConvert.SerializeObject(objectToPost, Formatting.Indented));
        }

        /// <summary>
        /// Posts JSON to the URL specified.
        /// </summary>
        /// <param name="json">JSON.</param>
        /// <param name="url">URL.</param>
        public static void Post(string json, string url)
        {
            new WebClient().UploadString(url, "POST", json);
        }
    }
}