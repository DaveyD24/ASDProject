using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class Restaurant
    {
        public Restaurant()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

    }

    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext()
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}