using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
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
        /// Gets string from URL.
        /// </summary>
        /// <param name="url">URL to get the string from.</param>
        /// <returns>String downloaded from the URL.</returns>
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


        public static TResponse Get<TResponse>(string url)
        {
            try
            {
                HttpResponseMessage httpResponseMessage
                    = new HttpClient().GetAsync(url).Result;
                httpResponseMessage.EnsureSuccessStatusCode();

                return DeserializeString<TResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads string from the URL.
        /// </summary>
        /// <param name="url">URL to download the string from.</param>
        /// <returns>String downloaded from the URL.</returns>
        public static async Task<string> GetAsync(string url)
        {
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
        /// Downloads string from the URL using HttpClient.
        /// </summary>
        /// <param name="url">URL to download the JSON from.</param>
        /// <param name="httpClient">Optional. Customized HttpClient.</param>
        /// <returns>String downloaded from the URL.</returns>
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
        /// Serializes the object and posts it to the URL using specified HttpClient.
        /// </summary>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <returns>HttpResponseMessage.</returns>
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
        /// Serializes the object and posts it to the URL using specified HttpClient.
        /// </summary>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <returns>HttpResponseMessage.</returns>
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
        /// Posts JSON to the URL using the specified HttpClient.
        /// </summary>
        /// <typeparam name="TResponse">Type to parse the reponding JSON string into.</typeparam>
        /// <param name="json">JSON string to be posted.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <returns>Returns object of type TResponse.</returns
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
        /// Posts JSON to the URL using the specified HttpClient.
        /// </summary>
        /// <typeparam name="TResponse">Type to parse the reponding JSON string into.</typeparam>
        /// <param name="json">JSON string to be posted.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <returns>Returns object of type TResponse.</returns>
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
        /// Serializes the object and posts it to the URL.
        /// </summary>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <returns>HttpResponseMessage.</returns>
        public static HttpResponseMessage Post(object objectToPost, string url)
        {
            try
            {
                HttpResponseMessage response = new HttpClient().PostAsJsonAsync(new Uri(url), objectToPost).Result;
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Serializes the object and posts it to the URL.
        /// </summary>
        /// <param name="objectToPost">Object to serialize and post.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <returns>HttpResponseMessage.</returns>
        public static async Task<HttpResponseMessage> PostAsync(object objectToPost, string url)
        {
            try
            {
                HttpResponseMessage response = await new HttpClient().PostAsJsonAsync(new Uri(url), objectToPost);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Posts JSON string to the URL.
        /// </summary>
        /// <typeparam name="TResponse">Type to parse the reponding JSON string into.</typeparam>
        /// <param name="json">JSON string to be posted.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <returns>Returns object of type TResponse.</returns>
        public static TResponse Post<TResponse>(string json, string url)
        {
            try
            {
                HttpResponseMessage response = new HttpClient().PostAsync(new Uri(url), new StringContent(json)).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<TResponse>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TPostObject"></typeparam>
        /// <param name="postObject"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static TResponse Post<TResponse, TPostObject>(TPostObject postObject, string url)
        {
            try
            {
                HttpResponseMessage httpResponseMessage
                    = new HttpClient().PostAsync
                    (
                        url,
                        new StringContent
                        (
                            GetJson<TPostObject>(postObject),
                            Encoding.UTF8,
                            "application/json"
                        )
                    ).Result;
                httpResponseMessage.EnsureSuccessStatusCode();

                return DeserializeString<TResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToBeSerialized"></param>
        /// <returns></returns>
        public static string GetJson<T>(T objectToBeSerialized)
        {
            MemoryStream ms = new MemoryStream();
            new DataContractJsonSerializer(typeof(T)).WriteObject(ms, objectToBeSerialized);
            byte[] jsonBytes = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(jsonBytes, 0, jsonBytes.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonToBeDeserialized"></param>
        /// <returns></returns>
        public static T GetObject<T>(string jsonToBeDeserialized)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonToBeDeserialized));
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            T temp = (T)dataContractJsonSerializer.ReadObject(memoryStream);
            memoryStream.Close();
            return temp;
        }

        /// <summary>
        /// Posts JSON string to the URL.
        /// </summary>
        /// <typeparam name="TResponse">Type to parse the reponding JSON string into.</typeparam>
        /// <param name="json">JSON string to be posted.</param>
        /// <param name="url">URL to post JSON string to.</param>
        /// <returns>Returns object of type TResponse.</returns>
        public static async Task<TResponse> PostAsync<TResponse>(string json, string url)
        {
            try
            {
                HttpResponseMessage response = await new HttpClient().PostAsync(new Uri(url), new StringContent(json));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TResponse>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}