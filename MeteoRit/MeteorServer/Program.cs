using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace MeteorServer
{
    class Program
    {
        public static SynchronizedCollection< Measurement> measurements = new SynchronizedCollection<Measurement>(); 

        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8734/service");

            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(RestService), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Default;
                host.Description.Behaviors.Add(smb);

                host.AddServiceEndpoint("MeteorServer.IRestService", new BasicHttpBinding(),
                    new Uri(baseAddress, "service2"));


                
                host.AddDefaultEndpoints();


                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
