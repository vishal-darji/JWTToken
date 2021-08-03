using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace ProductAPI
{
    public static class PlaceTransaction
    {
        [FunctionName("PlaceTransaction")]
        public static void Run([ServiceBusTrigger("orderplace", Connection = "serviceBus")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            OrderModel model = JsonConvert.DeserializeObject<OrderModel>(myQueueItem);

            CloudStorageAccount storageAccount =
   new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("redemption", "GMi6OAXfDBzeQmA5pQa6c6QQexNE1ZqIUl24CJbGHrpc/y9KCC8fuj7xK7Gbiy84agtm6E0fiQCrA5uNeBKUjw=="),
       true);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable _linkTable = tableClient.GetTableReference("PointsTable");
            PointsModel objPoints = new PointsModel();
            objPoints.UserName = model.UserName;
            objPoints.Type = "less";
            objPoints.Points = model.ProductCost;
            objPoints.ProductName = model.ProductName;
            TableOperation insertOperation = TableOperation.Insert(objPoints);
            
            try
            {

                 _linkTable.ExecuteAsync(insertOperation);

               
            }
            catch (Exception e)
            {
                log.LogInformation($"exception occured {e}");
            }
           


        }
    }
}
