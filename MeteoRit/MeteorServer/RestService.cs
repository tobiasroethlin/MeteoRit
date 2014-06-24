using System.Linq;
using System.ServiceModel.Web;

namespace MeteorServer
{
    class RestService : IRestService
    {
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "measurement")]
        public void CreateMeasurement(Measurement measurement)
        {
            Program.measurements.Add(measurement);
        }
        
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "measurement/{id}")]
        public Measurement GetMeasurement(string id, string timestamp)
        {
            return Program.measurements.SingleOrDefault(mes => mes.Id == int.Parse(id) && mes.Timestamp == long.Parse(timestamp));

        }
    }
}