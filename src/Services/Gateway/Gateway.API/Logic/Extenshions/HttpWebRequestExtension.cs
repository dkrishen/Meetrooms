using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.API.Logic.Extenshions
{
    public static class HttpWebRequestExtension
    {
        public static void AddRequestBody(this HttpWebRequest request, object data)
        {
            string content = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byteContent = encoding.GetBytes(content);
            request.ContentLength = byteContent.Length;
            request.ContentType = "application/json";

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteContent, 0, byteContent.Length);
            }
        }

        public static async Task<string> SendAsync(this HttpWebRequest request)
        {
            var result = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    result = await new StreamReader(stream).ReadToEndAsync();
                }
            }
            catch (Exception) { }
            return result;
        }
    }
}
