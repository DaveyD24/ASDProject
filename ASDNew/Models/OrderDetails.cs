using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class OrderDetails
    {
        public int Sno { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string DateOfOrder { get; set; }
        public string Email { get; set; }

    }
}