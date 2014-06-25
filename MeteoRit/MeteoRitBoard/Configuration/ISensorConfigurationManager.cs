namespace MeteoRitBoard.Configuration
{
    using MeteoRitBoard.Controllers;

    public interface ISensorConfigurationManager
    {
        SensorConfiguration SensorConfiguration { get; }

        void ListenForConfigurationChange();
    }
}