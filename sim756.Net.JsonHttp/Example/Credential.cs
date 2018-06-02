using System;
using System.Net;

namespace Example
{
    public class Credential : ICredentials
    {
        public NetworkCredential GetCredential(Uri uri, string authType)
        {
            return new NetworkCredential()
            {
                
            };
        }
    }
}