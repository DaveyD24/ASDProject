using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class Product
    {
        public Product()
        {

        }

        public int Id { get; set; }
        public Restaurant Restaurant { get; set; }
        public ProductCategory Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }

    public class ProductDBContext : DbContext
    {
        public ProductDBContext()
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}