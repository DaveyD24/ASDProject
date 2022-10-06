using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class Cart
    {
        public Cart()
        {

        }

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
        public int TotalCost { get; set; }
    }
}