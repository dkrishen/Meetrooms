using System.IO;
using System.Net;
using System;

namespace Gateway.API.Logic.Request
{
    public abstract class Request
    {
        protected string baseUrl;

        public Request(string url)
        {
            this.baseUrl = url;
        }
    }
}
