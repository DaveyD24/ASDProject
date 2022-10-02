using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{

    public enum Status
    {
        Unconfirmed,
        Confirmed,
        Processed,
        Shipped,
        Completed
    }
    public class Order
    {
        public Order()
        {

        }

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; } = Status.Unconfirmed;
    }

    public class OrderDBContext : DbContext
    {
        public OrderDBContext()
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}