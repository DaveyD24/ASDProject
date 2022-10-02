using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class ASDContext : DbContext
    {
        public ASDContext()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}