using Microsoft.SPOT;

namespace MeteoRitBoard
{
    using MeteoRitBoard.Controllers;
    using MeteoRitBoard.Networking;

    public partial class Program
    {
        private Bootstrapper bootstrapper;

        void ProgramStarted()
        {
            Debug.Print("Program Started");
            NetworkHelper.PrintLocalAddress();

            var barometerController = new BarometerController(this.barometer);
            var temperatureHumidityController = new TemperatureHumidityController(this.temperatureHumidity);

            this.bootstrapper = new Bootstrapper();
            this.bootstrapper.StartWebServer();
            this.bootstrapper.UseSensorController(barometerController);
            this.bootstrapper.UseSensorController(temperatureHumidityController);
        }
    }
}