using System;

namespace MeteoRitRichClient
{
    [Serializable]
    public class Measurement
    {
        public int Id { get; set; }

        public String CityName { get; set; }

        public long Timestamp { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public int Humidity { get; set; }
    }
}