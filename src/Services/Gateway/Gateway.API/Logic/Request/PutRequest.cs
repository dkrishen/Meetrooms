using Gateway.API.Logic.Extenshions;
using System.Net;

namespace Gateway.API.Logic.Request
{
    public class PutRequest : Request
    {
        public PutRequest(string url) : base(url)
        {
        }

        public bool Send(string requestUrl, string token = null, object data = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(baseUrl + requestUrl);
            request.Method = "PUT";

            if (data != null)
            {
                request.AddRequestBody(data);
            }

            if (token != null)
            {
                request.Headers.Add("Authorization: " + token);
            }

            return request.Send().DeserializeJSON<bool>();
        }
    }
}
