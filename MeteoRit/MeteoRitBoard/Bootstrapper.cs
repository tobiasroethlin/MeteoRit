namespace MeteoRitBoard
{
    using System.Collections;

    using Gadgeteer.Networking;

    using MeteoRitBoard.Configuration;
    using MeteoRitBoard.Controllers;
    using MeteoRitBoard.Networking;
    using MeteoRitBoard.REST;

    public class Bootstrapper
    {
        private readonly ISensorDataSender sensorDataSender;

        private readonly ArrayList sensorCommandListeners;

        private readonly ISensorConfigurationManager sensorConfigurationManager;

        public Bootstrapper()
        {
            this.sensorDataSender = new SensorDataSender(NetworkConfiguration.ServerAddress);
            this.sensorCommandListeners = new ArrayList();
            this.sensorConfigurationManager = new SensorConfigurationManager();
        }

        public void UseSensorController(ISensorController sensorController)
        {
            sensorController.NewSensorData += (sender, args) => this.sensorDataSender.SendSensorData(args.Type, args.Value);
            var sensorCommandListener = new SensorCommandListener(sensorController);
            this.sensorCommandListeners.Add(sensorCommandListener);
            sensorCommandListener.ListenForStartCommand();
            sensorCommandListener.ListenForStopCommand();
        }

        public void StartWebServer()
        {
            WebServer.StartLocalServer(NetworkConfiguration.LocalAddress, NetworkConfiguration.LocalPort);
        }
    }
}