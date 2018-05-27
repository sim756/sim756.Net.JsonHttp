using System;
using System.Collections.Generic;
using System.Text;

namespace sim756.Net.JsonHttp.Exceptions
{
    public class EmptyUrlException : Exception
    {
        public EmptyUrlException()
        {
        }

        public EmptyUrlException(string message) : base(message)
        {
        }

        public EmptyUrlException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}