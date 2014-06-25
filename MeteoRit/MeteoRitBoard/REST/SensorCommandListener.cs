namespace MeteoRitBoard.REST
{
    using Gadgeteer.Networking;

    using MeteoRitBoard.Configuration;
    using MeteoRitBoard.Controllers;

    public class SensorCommandListener : ISensorCommandListener
    {
        private readonly ISensorController sensorController;

        private WebEvent startEvent;

        private WebEvent stopEvent;

        public SensorCommandListener(ISensorController sensorController)
        {
            this.sensorController = sensorController;
        }

        public void ListenForStartCommand()
        {
            string path = this.sensorController.SensorName + "/start";
            this.startEvent = WebServer.SetupWebEvent(path);
            this.startEvent.WebEventReceived += this.OnStartEventReceived;
        }

        public void ListenForStopCommand()
        {
            string path = this.sensorController.SensorName + "/stop";
            this.stopEvent = WebServer.SetupWebEvent(path);
            this.stopEvent.WebEventReceived += this.OnStopEventReceived;
        }

        private void OnStopEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            this.sensorController.Stop();
            responder.Respond("Stopped " + this.sensorController.SensorName);
        }

        private void OnStartEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            this.sensorController.Start(SensorConfiguration.DefaultMeasuringInterval);
            responder.Respond("Started " + this.sensorController.SensorName);
        }
    }
}