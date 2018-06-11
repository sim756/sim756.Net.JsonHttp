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
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            //return httpClient.GetStringAsync(new Uri(url)).Result;
            try
            {
                HttpResponseMessage response = new HttpClient().GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from specified URL.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static async Task<string> GetAsync(string url)
        {
            //return (new WebClient()).DownloadString(url);
            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from specified URL using HttpClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="httpClient">Optional. Customized HttpClient.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static string Get(string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;

            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from specified URL using HttpClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="httpClient">Optional. Customized HttpClient.</param>
        /// <returns>Downloaded JSON in string.</returns>
        public static async Task<string> GetAsync(string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from the URL using default HttpClient and returns the object into type T.
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static T Deserialize<T>(string url)
        {
            try
            {
                HttpResponseMessage response = (new HttpClient()).GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from the URL using default HttpClient and returns the object into type T.
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static async Task<T> DeserializeAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await (new HttpClient()).GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from the URL. 
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="httpClient">Optional. Customized HttpClient.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static T Deserialize<T>(string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from the URL. 
        /// </summary>
        /// <typeparam name="T">Deserialize the Object into this type.</typeparam>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="httpClient">Optional. Customized HttpClient.</param>
        /// <returns>Deserialized object into type T.</returns>
        public static async Task<T> DeserializeAsync<T>(string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
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
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public static HttpResponseMessage Post(object objectToPost, string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(new Uri(url), objectToPost).Result;
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(object objectToPost, string url, HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(url), objectToPost);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Posts JSON to the URL using the specified WebClient.
        /// </summary>
        public static TResponse Post<TResponse>(string json, string url, HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new HttpClientException();
            }
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(new Uri(url), new StringContent(json)).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<TResponse>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Posts JSON to the URL using the specified WebClient.
        /// </summary>
        public static async Task<TResponse> PostAsync<TResponse>(string json, string url, HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new HttpClientException();
            }
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), new StringContent(json));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TResponse>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
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