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

    /// <summary>
    /// A simplified Generic method to GET or POST JSON using HTTP, and get it deserialized or serialized into the Object of specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonHttpClient<T>
    {
        /// <summary>
        /// Object that holds the object to be deserialized into or serialize form.
        /// </summary>
        public T Object { get; set; }

        /// <summary>
        /// URL to be posted to or get the JSON from.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Customized WebClient.
        /// </summary>
        public WebClient WebClient { get; set; }

        /// <summary>
        /// Customized HttpClient
        /// </summary>
        public HttpClient HttpClient { get; set; }

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
        public JsonHttpClient(string url, HttpClient httpClient)
        {
            this.Url = url;
            this.HttpClient = httpClient;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public T Deserialize()
        {
            try
            {
                HttpResponseMessage response = (this.HttpClient ?? new HttpClient()).GetAsync(Url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
                // string responseBody = await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Deserialize the object into the Object property downloading the JSON from URL (property) using WebClient (property).
        /// </summary>
        /// <returns>Deserialized object of type T.</returns>
        public async Task<T> DeserializeAsync()
        {
            try
            {
                HttpResponseMessage response = await (this.HttpClient ?? new HttpClient()).GetAsync(Url);
                response.EnsureSuccessStatusCode();
                return Object = await response.Content.ReadAsAsync<T>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public T Deserialize(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return Object = response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<T> DeserializeAsync(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public T Deserialize(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(Url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<T> DeserializeAsync(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keepUrl">Whether to assign url parameter to the Url property.</param>
        /// <returns></returns>
        public T Deserialize(string url, bool keepUrl = true)
        {
            try
            {
                HttpResponseMessage response = this.HttpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        private void IsKeepHttpClient(ref HttpClient httpClient, bool keepHttpClient)
        {
            if (keepHttpClient == true)
            {
                this.HttpClient = httpClient;
            }
        }

        /// <summary>
        /// Downloads JSON from Url (property), using a WebClient unless WebClient (property) is null and deserializes it into the type of T and assigns it into the Object property.
        /// </summary>
        public async void DeserializeInside()
        {
            try
            {
                HttpResponseMessage response = await (this.HttpClient ?? new HttpClient()).GetAsync(Url);
                response.EnsureSuccessStatusCode();
                Object = await response.Content.ReadAsAsync<T>();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// Downloads JSON from Url, deserializes it into the type of T and assigns it into the Object property.
        /// </summary>
        /// <param name="url">URL to download JSON from. Optional when Url Property is set, assign or left "null" to use Url property.</param>
        /// <param name="keepUrl">Whether to assign url parameter to the Url property.</param>
        public async Task DeserializeInsideAync(string url, bool keepUrl = true)
        {
            HttpResponseMessage response = await this.HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Object = await response.Content.ReadAsAsync<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keepUrl"></param>
        public void DeserializeInside(string url, bool keepUrl = true)
        {
            HttpResponseMessage response = this.HttpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Object = response.Content.ReadAsAsync<T>().Result;
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
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient">Whether to assign url parameter to the Url property.</param>
        public void DeserializeInside(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            HttpResponseMessage response = httpClient.GetAsync(Url).Result;
            response.EnsureSuccessStatusCode();
            Object = response.Content.ReadAsAsync<T>().Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        public async void DeserializeInsideAsync(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            HttpResponseMessage response = await httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            Object = await response.Content.ReadAsAsync<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepUrl">Whether to assign url parameter to the Url property.</param>
        /// <param name="keepHttpClient">Whether to assign url parameter to the Url property.</param>
        public void DeserializeInside(string url, HttpClient httpClient, bool keepUrl = false, bool keepHttpClient = false)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            Object = response.Content.ReadAsAsync<T>().Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        public async void DeserializeInsideAsync(string url, HttpClient httpClient, bool keepUrl = false, bool keepHttpClient = false)
        {
            IsKeepHttpClient(ref httpClient, keepHttpClient);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Object = await response.Content.ReadAsAsync<T>();
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
        /// <param name="keepUrl">Whether to assign url parameter to the Url property.</param>
        public void Post(string url, bool keepUrl = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="keepObject">Whether to assign object parameter to the Object property.</param>
        /// <param name="keepUrl">Whether to assign url parameter to the Url property.</param>
        public void Post(T objectToPost, string url, bool keepObject = true, bool keepUrl = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="keepObject">Whether to assign object parameter to the Object property.</param>
        public void Post(T objectToPost, bool keepObject = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webClient"></param>
        /// <param name="keepWebClient">Whether to assign WebClient parameter to the WebClient property.</param>
        public void Post(WebClient webClient, bool keepWebClient = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="keepObject">Whether to assign object parameter to the Object property.</param>
        /// <param name="webClient"></param>
        /// <param name="keepWebClient">Whether to assign webClient parameter to the WebClient property.</param>
        public void Post(T objectToPost, WebClient webClient, bool keepObject = true, bool keepWebClient = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<string> Post(T objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
                // string responseBody = await client.GetStringAsync(uri);
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
        /// <typeparam name="TPost"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<TResponse> Post<TResponse, TPost>(TPost objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
                // string responseBody = await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPost"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post<TPost>(TPost objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
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
    }
}