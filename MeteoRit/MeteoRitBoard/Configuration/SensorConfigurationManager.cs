namespace MeteoRitBoard.Configuration
{
    using System;

    using Gadgeteer.Networking;

    using MeteoRitBoard.Controllers;
    using MeteoRitBoard.Networking;

    public class SensorConfigurationManager
    {
        private const string ConfigurePath = "configure";

        private WebEvent configureEvent;

        public SensorConfigurationManager()
        {
            this.SensorConfiguration = new SensorConfiguration(SensorConfiguration.DefaultMeasureInterval);
        }

        public SensorConfiguration SensorConfiguration { get; set; }

        public void StartListeningForConfiguration()
        {
            this.configureEvent = WebServer.SetupWebEvent(ConfigurePath);
            this.configureEvent.WebEventReceived += this.OnConfigureEventReceived;
            WebServer.StartLocalServer(NetworkConfiguration.LocalAddress, NetworkConfiguration.LocalPort);
        }

        public void StopListeningForConfiguration()
        {
            this.configureEvent.WebEventReceived -= this.OnConfigureEventReceived;
            WebServer.StopLocalServer();
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
                }
            }
        }
    }
}