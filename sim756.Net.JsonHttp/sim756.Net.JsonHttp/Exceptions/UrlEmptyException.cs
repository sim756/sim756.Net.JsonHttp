using System;
using System.Collections.Generic;
using System.Text;

namespace sim756.Net.JsonHttp.Exceptions
{
    public class UrlEmptyException : Exception
    {
        public UrlEmptyException()
        {
        }

        public UrlEmptyException(string message) : base(message)
        {
        }

        public UrlEmptyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}