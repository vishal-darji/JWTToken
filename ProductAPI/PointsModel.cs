using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI
{
  
    public class PointsModel : TableEntity
    {
        // Set up Partition and Row Key information
        public PointsModel()
        {
            this.PartitionKey = "india";
            this.RowKey = Guid.NewGuid().ToString();
        }


       

        public string UserName { get; set; }
        public string Points { get; set; }
        public string Type { get; set; }

        public string ProductName { get; set; }

    }
}
