namespace MeteoRitBoard.Controllers
{
    using System;

    using Gadgeteer;
    using Gadgeteer.Modules.Seeed;

    public class TemperatureHumidityController : ISensorController
    {
        private readonly TemperatureHumidity temperatureHumidity;

        private Timer timer;

        public TemperatureHumidityController(TemperatureHumidity temperatureHumidity)
        {
            this.temperatureHumidity = temperatureHumidity;
        }

        public event SensorDataEventHandler NewSensorData;

        public void RequestMeasurement()
        {
            this.temperatureHumidity.RequestMeasurement();
        }

        public void Start(TimeSpan interval)
        {
            this.temperatureHumidity.MeasurementComplete += this.OnMeasurementComplete;
            timer = new Timer(interval);
            timer.Tick += OnRequestMeasurement;

        }

        public void Stop()
        {
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