using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class OrderHistory
    {
        public OrderHistory()
        {

        }
        [Key]
        public int Sno { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string DateofOrder { get; set; }

    }

    public class OrderHistoryDBContext : DbContext
    {
        public OrderHistoryDBContext()
        {

        }
        public DbSet<OrderHistory> OrderHistory { get; set; }
    }
}


