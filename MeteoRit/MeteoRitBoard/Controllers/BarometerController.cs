namespace MeteoRitBoard.Controllers
{
    using System;

    using Gadgeteer.Modules.Seeed;

    public class BarometerController : ISensorController
    {
        private readonly Barometer barometer;

        public BarometerController(Barometer barometer)
        {
            this.barometer = barometer;
        }

        public event SensorDataEventHandler NewSensorData;

        public void RequestMeasurement()
        {
            this.barometer.RequestMeasurement();
        }

        public void Start(TimeSpan interval)
        {
            this.barometer.ContinuousMeasurementInterval = interval;
            this.barometer.MeasurementComplete += this.OnMeasurementComplete;
            this.barometer.StartContinuousMeasurements();
        }

        public void Stop()
        {
            this.barometer.StopContinuousMeasurements();
            this.barometer.MeasurementComplete -= this.OnMeasurementComplete;
        }

        private void OnMeasurementComplete(Barometer sender, Barometer.SensorData sensorData)
        {
            var handler = this.NewSensorData;
            if (handler != null)
            {
                handler(this, new SensorDataEventArgs("Pressure", sensorData.Pressure));
            }
        }
    }
}