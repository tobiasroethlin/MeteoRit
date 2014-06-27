namespace MeteoRitBoard.Controllers
{
    using System;

    using Gadgeteer;
    using Gadgeteer.Modules.Seeed;

    using Microsoft.SPOT;

    public class TemperatureHumidityController : ISensorController
    {
        private readonly TemperatureHumidity temperatureHumidity;

        private Timer timer;

        public event SensorDataEventHandler NewSensorData;

        public TemperatureHumidityController(TemperatureHumidity temperatureHumidity)
        {
            this.temperatureHumidity = temperatureHumidity;
        }

        public string SensorName
        {
            get
            {
                return "temperature";
            }
        }

        public void RequestMeasurement()
        {
            this.temperatureHumidity.RequestMeasurement();
        }

        public void Start(TimeSpan interval)
        {
            Debug.Print("Starting " + this.SensorName);
            this.temperatureHumidity.MeasurementComplete += this.OnMeasurementComplete;
            timer = new Timer(interval);
            timer.Tick += OnRequestMeasurement;
        }

        public void Stop()
        {
            Debug.Print("Stopping " + this.SensorName);
            this.temperatureHumidity.MeasurementComplete -= this.OnMeasurementComplete;
            this.timer.Stop();
        }

        private void OnRequestMeasurement(Timer timer1)
        {
            this.RequestMeasurement();
        }

        private void OnMeasurementComplete(TemperatureHumidity sender, double temperature, double relativeHumidity)
        {
            var handler = this.NewSensorData;
            if (handler != null)
            {
                handler(this, new SensorDataEventArgs("Temperature", temperature));
                handler(this, new SensorDataEventArgs("Humidity", relativeHumidity));
            }
        }
    }
}