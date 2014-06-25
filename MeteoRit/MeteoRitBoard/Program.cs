using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace MeteoRitBoard
{
    using MeteoRitBoard.Configuration;
    using MeteoRitBoard.Controllers;
    using MeteoRitBoard.Networking;
    using MeteoRitBoard.REST;

    public partial class Program
    {
        private SensorConfigurationManager sensorConfigurationManager;

        void ProgramStarted()
        {
            Debug.Print("Program Started");
            NetworkHelper.PrintLocalAddress();

            this.sensorConfigurationManager = new SensorConfigurationManager();
            this.sensorConfigurationManager.StartListeningForConfiguration();

            var restClient = new RestClient(NetworkConfiguration.ServerAddress);
            var barometerController = new BarometerController(this.barometer);
            
            barometerController.NewSensorData +=
                (sender, args) => restClient.SendSensorData(args.Type, args.Value);
            barometerController.Start(SensorConfiguration.DefaultMeasureInterval);

            var temperatureHumidityController = new TemperatureHumidityController(this.temperatureHumidity);
            temperatureHumidityController.NewSensorData +=
                (sender, args) => restClient.SendSensorData(args.Type, args.Value);
            temperatureHumidityController.Start(SensorConfiguration.DefaultMeasureInterval);
        }
    }
}