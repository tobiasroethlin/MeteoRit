using Microsoft.SPOT;

using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace MeteoRitBoard
{
    using MeteoRitBoard.Controllers;
    using MeteoRitBoard.Networking;
    using MeteoRitBoard.REST;

    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            NetworkHelper.PrintLocalAddress();

            var restClient = new RestClient(Configuration.Configuration.ServerAddress);
            var barometerController = new BarometerController(this.barometer);
            
            barometerController.NewSensorData +=
                (sender, args) => restClient.SendSensorData(args.Type, args.Value);
            barometerController.Start(SensorConfiguration.DefaultMeasureInterval);
        }
    }
}