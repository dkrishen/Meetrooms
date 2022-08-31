using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace MRA.Gateway.Repository
{
    public class RepositoryBase
    {
        private string apiUrl;

        public RepositoryBase(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        private string makeRequest(HttpWebRequest request)
        {
            var result = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    result = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception) { }
            return result;
        }

        protected string Request(string requestUrl, string requestMethod, object data = null, string token = null)
        {
            string content = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(apiUrl + requestUrl + content);
            request.Method = requestMethod;
           
            if(token != null)
            {
                request.Headers.Add("Authorization: " + token);
            }

            return makeRequest(request);
        }
    }
}
