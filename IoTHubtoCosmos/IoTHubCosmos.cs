using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventHubs;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IoTHubtoCosmos
{
    public static class IoTHubToCosmos
    {
        [FunctionName("IoTHubtoCosmos")]
        public static void Run([IoTHubTrigger("messages/events"
            , Connection = "hubcon"
            , ConsumerGroup ="myFunctionConsumerGroup")
            ]EventData message
            , [CosmosDB(
             databaseName:"cartdemo",
                collectionName: "deleteme",
            
                ConnectionStringSetting = "CosmosDBConnection")] out dynamic document,
            ILogger log)
        {
            var mess = Encoding.UTF8.GetString(message.Body.Array);

            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
            var sensorData = Newtonsoft.Json.JsonConvert.DeserializeObject<Environment>(mess);

            document = sensorData;

            log.LogInformation($"C# Queue trigger function inserted one row");

        }
    }

    public class Environment
    {
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string device_type { get; set; }
    }


}
