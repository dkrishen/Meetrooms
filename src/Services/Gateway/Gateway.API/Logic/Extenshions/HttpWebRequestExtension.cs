using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

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

        public static string Send(this HttpWebRequest request)
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
    }
}
