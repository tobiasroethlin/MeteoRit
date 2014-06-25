namespace MeteoRitBoard.Configuration
{
    using System;

    using Gadgeteer.Networking;

    using MeteoRitBoard.Controllers;

    public class SensorConfigurationManager : ISensorConfigurationManager
    {
        private const string ConfigurePath = "configure";

        private WebEvent configureEvent;

        public SensorConfigurationManager()
        {
            this.SensorConfiguration = new SensorConfiguration(SensorConfiguration.DefaultMeasuringInterval);
        }

        public SensorConfiguration SensorConfiguration { get; private set; }

        public void ListenForConfigurationChange()
        {
            this.configureEvent = WebServer.SetupWebEvent(ConfigurePath);
            this.configureEvent.WebEventReceived += this.OnConfigureEventReceived;
        }

        private void OnConfigureEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            string value = responder.GetParameterValueFromURL("interval");
            if (value != null)
            {
                var interval = (int)double.Parse(value);
                if (interval > 0)
                {
                    this.SensorConfiguration = new SensorConfiguration(new TimeSpan(0, 0, 0, interval));
                    //TODO throw event
                }
            }
        }
    }
}