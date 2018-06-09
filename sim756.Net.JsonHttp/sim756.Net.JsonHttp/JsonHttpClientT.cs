using System;
using System.Collections.Generic;
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
            IsKeepHttpClient(httpClient, keepHttpClient);
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
            IsKeepHttpClient(httpClient, keepHttpClient);
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
            IsKeepHttpClient(httpClient, keepHttpClient);
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
            IsKeepHttpClient(httpClient, keepHttpClient);
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
            IsKeepHttpClient(httpClient, keepHttpClient);
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
            IsKeepHttpClient(httpClient, keepHttpClient);
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Object = await response.Content.ReadAsAsync<T>();
        }

        #region Object only

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="keepObject"></param>        
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(T objectToPost, bool keepObject = true)
        {
            IsKeepObject(objectToPost, keepObject);
            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(this.Url), objectToPost);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        /// <param name="keepObject"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(T objectToPost, bool keepObject = true)
        {
            IsKeepObject(objectToPost, keepObject);
            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(this.Url), objectToPost);
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
        /// <param name="keepObject"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(T objectToPost, bool keepObject = true)
        {
            IsKeepObject(objectToPost, keepObject);

            try
            {
                HttpResponseMessage response = this.HttpClient.PostAsJsonAsync(new Uri(this.Url), objectToPost).Result;
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
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="keepObject"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(T objectToPost, bool keepObject = true)
        {
            IsKeepObject(objectToPost, keepObject);
            try
            {
                HttpResponseMessage response = this.HttpClient.GetAsync(this.Url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        #endregion

        #region URL only

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(string url, bool keepUrl = true)
        {
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(url), this.Object);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string url, bool keepUrl = false)
        {
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(url), this.Object);
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
        /// <param name="url"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(string url, bool keepUrl = false)
        {
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = this.HttpClient.PostAsJsonAsync(new Uri(url), this.Object).Result;
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
        /// <typeparam name="TResponse"></typeparam>        
        /// <param name="url"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(string url, bool keepUrl = false)
        {
            IsKeepUrl(url, keepUrl);
            try
            {
                HttpResponseMessage response = this.HttpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }


        #endregion

        #region URL and HttpClient

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>        
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(url), this.Object);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(url), this.Object);
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
        /// <param name="url"></param>
        /// <param name="httpClient"></param>        
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);

            try
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(new Uri(url), this.Object).Result;
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
        /// <typeparam name="TResponse"></typeparam>        
        /// <param name="url"></param>
        /// <param name="httpClient"></param>        
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(string url, HttpClient httpClient, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        #endregion

        #region Object and URL

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(T objectToPost, string url, bool keepObject = true, bool keepUrl = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);
            
            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(url), objectToPost);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(T objectToPost, string url, bool keepObject = true, bool keepUrl = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(url), objectToPost);
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
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(T objectToPost, string url, bool keepObject = true, bool keepUrl = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = this.HttpClient.PostAsJsonAsync(new Uri(url), objectToPost).Result;
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
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(T objectToPost, string url, bool keepObject = true, bool keepUrl = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);

            try
            {
                HttpResponseMessage response = this.HttpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        #endregion

        #region HttpClient only

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(httpClient, keepHttpClient);

            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(this.Url), this.Object);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        public async Task<HttpResponseMessage> PostAsync(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(httpClient, keepHttpClient);

            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(this.Url), this.Object);
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
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(httpClient, keepHttpClient);

            try
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(new Uri(this.Url), this.Object).Result;
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
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(HttpClient httpClient, bool keepHttpClient = true)
        {
            IsKeepHttpClient(httpClient, keepHttpClient);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(this.Url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        #endregion

        #region all - Object, URL, HttpClient

        /// <summary>
        /// 
        /// </summary>
        public HttpResponseMessage Post()
        {
            try
            {
                HttpResponseMessage response = this.HttpClient.PostAsJsonAsync(new Uri(this.Url), this.Object).Result;
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
        public async Task<HttpResponseMessage> PostAsync()
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync(new Uri(this.Url), this.Object);
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
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse>(T objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri(url), objectToPost);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
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
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(T objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
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
        /// 
        /// </summary>
        /// <typeparam name="TPost"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(T objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);

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
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="objectToPost"></param>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <param name="keepObject"></param>
        /// <param name="keepUrl"></param>
        /// <param name="keepHttpClient"></param>
        /// <returns></returns>
        public TResponse Post<TResponse>(T objectToPost, string url, HttpClient httpClient, bool keepObject = true, bool keepUrl = true, bool keepHttpClient = true)
        {
            IsKeepObject(objectToPost, keepObject);
            IsKeepUrl(url, keepUrl);
            IsKeepHttpClient(httpClient, keepHttpClient);
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        #endregion

        #region internals

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToPost"></param>
        /// <param name="keepObject"></param>
        private void IsKeepObject(T objectToPost, bool keepObject)
        {
            if (keepObject == true)
            {
                this.Object = objectToPost;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="keepHttpClient"></param>
        private void IsKeepHttpClient(HttpClient httpClient, bool keepHttpClient)
        {
            if (keepHttpClient == true)
            {
                this.HttpClient = httpClient;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="keepUrl"></param>
        private void IsKeepUrl(string url, bool keepUrl)
        {
            if (keepUrl == true)
            {
                this.Url = url;
            }
        }

        #endregion
    }
}