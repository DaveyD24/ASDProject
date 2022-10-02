using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASDNew.Models
{
    public class Payment
    {
        public Payment()
        {

        }

        public int Id { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingSuburb { get; set; }
        public string BillingState { get; set; }
        public string BillingPostCode { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiry { get; set; }
        public int CVV { get; set; }
    }

    public class PaymentDBContext : DbContext
    {
        public PaymentDBContext()
        {

        }
        public DbSet<Payment> Payments { get; set; }
    }
}