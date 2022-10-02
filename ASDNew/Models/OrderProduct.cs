using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class OrderProduct
    {
        public OrderProduct()
        {

        }

        public int Id { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}