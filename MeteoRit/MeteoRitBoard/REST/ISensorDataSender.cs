namespace MeteoRitBoard.REST
{
    public interface ISensorDataSender
    {
        void SendSensorData(string measurementType, double measurement);
    }
}