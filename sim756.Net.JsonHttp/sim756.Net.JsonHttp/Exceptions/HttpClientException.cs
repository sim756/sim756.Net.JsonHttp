using System;

namespace sim756.Net.JsonHttp.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpClientException()
        {
        }

        public HttpClientException(string message) : base(message)
        {
        }

        public HttpClientException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}