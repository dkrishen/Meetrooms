using Gateway.API.Logic.Extenshions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace Gateway.API.Logic.Request
{
    public class GetRequest : Request
    {
        public GetRequest(string url) : base(url)
        {
        }

        public T Send<T>(string requestUrl, string token = null, object data = null)
        {
            HttpWebRequest request;

            if (data == null)
            {
                request = (HttpWebRequest)WebRequest.Create(baseUrl + requestUrl);
            } else
            {
                IDictionary<string, string> parameters = CreateParameterDictionary(data);
                request = (HttpWebRequest)WebRequest.Create(baseUrl + requestUrl + "?" + GetQueryString(parameters));
            }

            request.Method = "GET";

            if (token != null)
            {
                request.Headers.Add("Authorization: " + token);
            }

            return request.Send().DeserializeJSON<T>();
        }

        private IDictionary<string, string> CreateParameterDictionary(object data)
        {
            var json = data.SerializeToJson();

            return new Dictionary<string, string>()
            {
                { "data", json }
            };
        }

        private string GetQueryString(IDictionary<string, string> content)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            foreach (var key in content.Keys)
            {
                queryString.Add(key, content[key]);
            }

            return queryString.ToString();
        }
    }
}
