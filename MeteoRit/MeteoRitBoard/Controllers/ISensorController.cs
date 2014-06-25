namespace MeteoRitBoard.Controllers
{
    using System;

    public delegate void SensorDataEventHandler(ISensorController sender, SensorDataEventArgs dataEventArgs);

    public interface ISensorController
    {
        event SensorDataEventHandler NewSensorData;

        string SensorName { get; }

        void RequestMeasurement();

        void Start(TimeSpan interval);

        void Stop();
    }
}