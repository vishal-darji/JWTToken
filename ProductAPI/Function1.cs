using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProductAPI
{
    public static class Function1
    {
        [FunctionName("GetProducts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
              List<ProductModel> _Products = new List<ProductModel>();
        log.LogInformation("C# HTTP trigger function processed a request.");


            _Products.Add(new ProductModel { Id = 1, ProductCost = 3000, ProductName = "Jugg" });
            _Products.Add(new ProductModel { Id = 2, ProductCost = 5000, ProductName = "Mobile" });
            _Products.Add(new ProductModel { Id = 3, ProductCost = 8000, ProductName = "tablet" });
            return new OkObjectResult(_Products);
        }
    }
}
