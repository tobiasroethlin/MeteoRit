namespace XamarinCore.ViewModels
{
    using Common;

    public class MeteoRitViewModel
    {
        private ServiceCaller serviceCaller;

        public MeteoRitViewModel()
        {
            serviceCaller = new ServiceCaller();
        }


        public void UpdateMeteoRitData()
        {
            serviceCaller.measureEvent += (m) =>
            {
                var measuremnt = (Measurement)m;
                Id = measuremnt.Id;
                CityName = measuremnt.CityName;
                Timestamp = measuremnt.Timestamp;
                Temperature = measuremnt.Temperature;
                Pressure = measuremnt.Pressure;
                Humidity = measuremnt.Humidity;
            };

            var request = serviceCaller.CreateRequest("14", string.Empty);
            serviceCaller.GetMeasurement(request);
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public long Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }
}