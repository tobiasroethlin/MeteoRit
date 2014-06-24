namespace MeteoRitBoard.Controllers
{
    using Microsoft.SPOT;

    public class SensorDataEventArgs : EventArgs
    {
        public SensorDataEventArgs(string type, double value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }

        public double Value { get; private set; }
    }
}