using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Common
{
    public class ServiceCaller
    {
        public HttpWebRequest CreateCityRequest()
        {
            string URI = "http://192.168.1.71:9080/meteorit/REST/city/all";
            Uri serviceEndPoint;
            
            serviceEndPoint = new Uri(URI);
           
            HttpWebRequest request = WebRequest.CreateHttp(serviceEndPoint);
            request.ContentType = "application/json; charset=utf-8";

            return request;
        }

        public async Task<Measurement> GetMeasurement(string id, string timestamp)
        {
            string URI = "http://192.168.1.71:8080/meteorit/REST/measurement";
            Uri serviceEndPoint;
            if (!string.IsNullOrEmpty(timestamp))
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/" + timestamp + "/");
            }
            else
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/");
            }
            using (var httpClient = new System.Net.HttpClient())
            {
                var response = await httpClient.GetAsync(serviceEndPoint).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Measurement>().ConfigureAwait(false);
            }
        }
    
        public HttpWebRequest CreateRequest(string Id, string timestamp)
        {
            string URI = "http://192.168.1.71:8080/meteorit/REST/measurement";
            Uri serviceEndPoint;
            if (!string.IsNullOrEmpty(timestamp))
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/" + timestamp + "/");
            }
            else
            {
                serviceEndPoint = new Uri(URI + "/" + Id + "/");
            }

            HttpWebRequest request = HttpWebRequest.CreateHttp(serviceEndPoint);
            request.ContentType = "application/json";
            request.Method = "GET";
            
            return request;
        }
        
        public event MeasureEvent measureEvent;

        public void GetMeasurement<T>(HttpWebRequest httpReq)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));

            httpReq.BeginGetResponse ((ar) => 
            {
               var request = (HttpWebRequest)ar.AsyncState;
               using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                    {       
                          var s = response.GetResponseStream();
                          var obj = (T)jsonSerializer.ReadObject(s);
                          this.measureEvent(obj); 
                    }}, 
                    httpReq);
        }
    }

    public delegate void MeasureEvent(object sender);
}