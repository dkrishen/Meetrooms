﻿using Gateway.API.Logic.Extenshions;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.API.Logic.Request
{
    public class DeleteRequest : Request
    {
        public DeleteRequest(string url) : base(url)
        {
        }

        public async Task<bool> SendAsync(string requestUrl, string token = null, object data = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(baseUrl + requestUrl);
            request.Method = "DELETE";

            if (data != null)
            {
                request.AddRequestBody(data);
            }

            if (token != null)
            {
                request.Headers.Add("Authorization: " + token);
            }

            return (await request.SendAsync()).DeserializeJSON<bool>();
        }
    }
}