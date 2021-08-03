using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Azure.Messaging.ServiceBus;

namespace ProductAPI
{
    public static class OrderAPI
    {
        [FunctionName("OrderAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OrderModel data = JsonConvert.DeserializeObject<OrderModel>(requestBody);
            string connectionString = "Endpoint=sb://vishalservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mrp9x1IhbeSSkFd6VLqIP0VJe3PI3lFcfUI6iNJYD7s=";
            ServiceBusConnectionStringBuilder conStr;
            ServiceBusSender sender;

            ServiceBusClient client;
            var result = await GetPoints.Run(req, log);
            int points = Convert.ToInt32( ((ObjectResult)result).Value);
            client = new ServiceBusClient(connectionString);
                sender = client.CreateSender("orderplace");
            if(points < Convert.ToInt32(data.ProductCost))
            {

                return new OkObjectResult($" Cost of product is {data.ProductCost} but points available are {points}");
            }
                try
                {
                    // Use the producer client to send the batch of messages to the Service Bus queue
                    await sender.SendMessageAsync(new ServiceBusMessage(JsonConvert.SerializeObject(data)));
                    Console.WriteLine($"A  messages has been published to the queue.");
                    return new OkObjectResult("Azure Queue Message created Successfully");
                }
                catch (Exception exe)
                {
                    Console.WriteLine("{0}", exe.Message);
                    Console.WriteLine("Please restart application ");
                    return new OkObjectResult("Azure Queue Message failed");
                }
                finally
                {
                    // Calling DisposeAsync on client types is required to ensure that network
                    // resources and other unmanaged objects are properly cleaned up.
                    await sender.DisposeAsync();
                    await client.DisposeAsync();

                }
            
           


           
        }
    }
}
