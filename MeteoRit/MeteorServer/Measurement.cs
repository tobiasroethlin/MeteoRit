using System;

namespace MeteorServer
{
    [Flags]
    public enum  measured
    {
        Temperature = 1,
        Pressure = 2,
        Humidity = 4,
    }
}