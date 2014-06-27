namespace MeteoRitBoard.Networking
{
    using Microsoft.SPOT.Net.NetworkInformation;

    public class NetworkConfiguration
    {
        public const string ServerAddress = "http://192.168.1.71:8080/meteorit/REST/datapoint/";

        public static string LocalAddress
        {
            get
            {
                return NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress;
            }
        }

        public static int LocalPort
        {
            get
            {
                return 8080;
            }
        }
    }
}