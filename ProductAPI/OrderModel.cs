using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI
{
   public class OrderModel
    {
        public int Id { get; set; }

        public string ProductCost { get; set; }

        public string ProductQty { get; set; }
        public string ProductName { get; set; }

        public string UserName { get; set; }
    }
}
