using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Common
{
    public class ServiceCaller
    {
        public Measurement Measurement;
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

            HttpWebRequest request = WebRequest.CreateHttp(serviceEndPoint);
            request.ContentType = "application/json; charset=utf-8";

            return request;
        }
        
        public event MeasureEvent measureEvent;

        public void GetMeasurement(HttpWebRequest httpReq)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Measurement));

            httpReq.BeginGetResponse ((ar) => 
            {
               var request = (HttpWebRequest)ar.AsyncState;
                WaitHandle.WaitAll(new[] {ar.AsyncWaitHandle});
               using (var response = (HttpWebResponse)request.EndGetResponse(ar))
                    {       
                          var s = response.GetResponseStream();
                          this.Measurement = (Measurement)jsonSerializer.ReadObject(s);
                          this.measureEvent(Measurement); 
                    }}, 
                    httpReq);
            
        }

    }

    public delegate void MeasureEvent(object sender);
}