using System;

namespace sim756.Net.JsonHttp.Exceptions
{
    public class WebClientException : Exception
    {
        public WebClientException()
        {
        }

        public WebClientException(string message) : base(message)
        {
        }

        public WebClientException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}