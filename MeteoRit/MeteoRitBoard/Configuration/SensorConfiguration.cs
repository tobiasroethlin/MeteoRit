namespace MeteoRitBoard.Configuration
{
    using System;

    public class SensorConfiguration
    {
        public static TimeSpan DefaultMeasuringInterval = new TimeSpan(0, 0, 0, 5);

        public SensorConfiguration(TimeSpan measureInterval)
        {
            this.MeasuringInterval = measureInterval;
        }

        public TimeSpan MeasuringInterval { get; private set; }
    }
}