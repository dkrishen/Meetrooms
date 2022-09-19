using Gateway.API.Logic.Extenshions;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Gateway.API.Logic.Request
{
    public class PostRequest : Request
    {
        public PostRequest(string url) : base(url)
        {
        }

        public bool Send(string requestUrl, string token = null, object data = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(baseUrl + requestUrl);
            request.Method = "POST";

            if(data != null)
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
