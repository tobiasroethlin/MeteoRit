using System.ServiceModel;

namespace MeteorServer
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        void CreateMeasurement(Measurement measurement);

        Measurement GetMeasurement(string id, string timestamp);
    }
}