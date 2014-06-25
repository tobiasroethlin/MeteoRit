namespace MeteoRitBoard.Controllers
{
    using System;

    public class SensorConfiguration
    {
        public static TimeSpan DefaultMeasureInterval = new TimeSpan(0, 0, 0, 5);

        public SensorConfiguration(TimeSpan measureInterval)
        {
            this.MeasureInterval = measureInterval;
        }

        public TimeSpan MeasureInterval { get; private set; }
    }
}