using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace MeteoRitRichClient
{
    public class ServiceCaller
    {
        public HttpWebRequest CreateRequest(string URI, string Id, string timestamp)
        {
            Uri serviceEndPoint;
            if (!string.IsNullOrEmpty(timestamp))
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/" + timestamp + "/");
            }
            else
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/");
            }

            HttpWebRequest request = WebRequest.CreateHttp(serviceEndPoint);

            request.Headers.Add("Authorizaion", "APIKey " + 123);
            request.ContentType = "application/json; charset=utf-8";

            return request;
        }

        public HttpWebResponse GetResponse(HttpWebRequest request, string jsonData)
        {
            return (HttpWebResponse)request.GetResponse();
        }

        public Measurement GetMeasurement(HttpWebRequest request)
        {
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = reader.ReadToEnd();
                Measurement myojb = (Measurement)js.Deserialize(objText, typeof(Measurement));
                return myojb;
            }
        }
    }
}