using System;

namespace sim756.Net.JsonHttp.Exceptions
{
    public class DeprecatedException : Exception
    {
        public DeprecatedException()
        {
        }

        public DeprecatedException(string message) : base(message)
        {
        }

        public DeprecatedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}