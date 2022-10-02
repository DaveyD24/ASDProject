using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class Customer
    {

        public Customer()
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }

    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext()
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}