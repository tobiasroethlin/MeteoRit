namespace MeteoRitBoard.REST
{
    using System.Collections;

    using MeteoRitBoard.Networking;

    public interface ISensorCommandListener
    {
        void ListenForStartCommand();

        void ListenForStopCommand();
    }
}