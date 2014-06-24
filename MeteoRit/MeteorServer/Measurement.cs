using System;

namespace MeteorServer
{
    public class Measurement
    {
        public int Id { get; set; }

        public String CityName { get; set; }

        public long Timestamp { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public int Humidity { get; set; }

        public measured Measure { get; set; }
    }

    [Flags]
    public enum  measured
    {
        Temperature = 1,
        Pressure = 2,
        Humidity = 4,
    }
}