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

        [Required(ErrorMessage = "field cannot be empty")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "cannot contain numbers")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage="Invalid password")]
        public string Password { get; set; }

        //This might not be necessary
        //public List<Product> Products { get; set; }


    }

    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext()
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}