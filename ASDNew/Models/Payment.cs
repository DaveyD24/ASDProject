using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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

        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "Cannot contain numbers")]
        public string BillingName { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string BillingEmail { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"\d{1,3}", ErrorMessage = "Must be valid number between 1 and 999")]
        public string BillingStreetNum { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        public string BillingStreet { get; set; }

        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "Cannot contain numbers")]
        public string BillingSuburb { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "Cannot contain numbers")]

        public string BillingState { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"\d{4}", ErrorMessage = "Must contain 4 digits")]
        public string BillingPostCode { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"^([^0-9]*)$", ErrorMessage = "Cannot contain numbers")]
        public string CreditCardName { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"\d{16}", ErrorMessage = "Must contain 16 digits with no spaces")]
        public string CreditCardNumber { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"(0[1-9]|10|11|12)/202[3-5]$", ErrorMessage = "Must expire between 2023 and 2025. Format: MM/YYYY")]
        public string CreditCardExpiry { get; set; }


        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Must contain 3 digits")]
        public string CVV { get; set; }


        public DateTime Date { get; set; }
    }

    public class PaymentDBContext : DbContext
    {
        public PaymentDBContext()
        {

        }
        public DbSet<Payment> Payments { get; set; }
    }
}