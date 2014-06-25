namespace MeteoRitBoard.REST

{
    using Gadgeteer.Networking;

    using Json.NETMF;

    using Microsoft.SPOT;

    public class SensorDataSender : ISensorDataSender
    {
        private const string MimeJson = "application/json";

        private readonly string address;

        public SensorDataSender(string address)
        {
            this.address = address;
        }

        public void SendSensorData(string measurementType, double measurement)
        {
            var data = new Data { Type = measurementType, Value = measurement };
            var jsonData = JsonSerializer.SerializeObject(data);
            var postContent = POSTContent.CreateTextBasedContent(jsonData);
            var postRequest = HttpHelper.CreateHttpPostRequest(this.address, postContent, MimeJson);

            Debug.Print("Sending " + measurementType +  ": " + measurement);
            postRequest.ResponseReceived += (sender, response) => Debug.Print("Response received with code: " + response.StatusCode);
            postRequest.SendRequest();
        }

        private class Data
        {
            public string Type { get; set; }

            public double Value { get; set; }
        }
    }
}