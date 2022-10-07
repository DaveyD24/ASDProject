using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public class CartDBContext : DbContext
        {
            public CartDBContext()
            {

            }
            public DbSet<Cart> Cart { get; set; }
        }
    }
}
