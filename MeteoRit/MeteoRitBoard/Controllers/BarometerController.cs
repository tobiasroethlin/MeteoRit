namespace MeteoRitBoard.Controllers
{
    using System;

    using Gadgeteer.Modules.Seeed;

    using Microsoft.SPOT;

    public class BarometerController : ISensorController
    {
        private readonly Barometer barometer;

        public event SensorDataEventHandler NewSensorData;

        public BarometerController(Barometer barometer)
        {
            this.barometer = barometer;
        }

        public string SensorName
        {
            get
            {
                return "barometer";
            }
        }

        public void RequestMeasurement()
        {
            this.barometer.RequestMeasurement();
        }

        public void Start(TimeSpan interval)
        {
            Debug.Print("Starting " + this.SensorName);
            this.barometer.ContinuousMeasurementInterval = interval;
            this.barometer.MeasurementComplete += this.OnMeasurementComplete;
            this.barometer.StartContinuousMeasurements();
        }

        public void Stop()
        {
            Debug.Print("Stopping " + this.SensorName);
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