namespace MeteoRitBoard.Networking
{
    using Microsoft.SPOT;
    using Microsoft.SPOT.Net.NetworkInformation;

    public class NetworkHelper
    {
        public static void PrintLocalAddress()
        {
            foreach (var network in NetworkInterface.GetAllNetworkInterfaces())
            {
                Debug.Print("Local IP Address: " + network.IPAddress);
            }
        }
    }
}