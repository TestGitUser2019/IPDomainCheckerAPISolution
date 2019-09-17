using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPDomainChecker.Infrastructure
{
    public class RestHttpResponseException : Exception
    {
        public RestHttpResponseException(HttpStatusCode statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string Content { get; private set; }
    }
}
