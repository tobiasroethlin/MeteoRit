namespace MeteoRitBoard.REST
{
    public interface IRestClient
    {
        void SendSensorData(string measurementType, double measurement);
    }
}