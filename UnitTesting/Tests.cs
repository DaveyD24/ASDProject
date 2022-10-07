using ASDNew.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        //Patrick.E
        //F109: Adding products to restaurant
        [Test]
        public void TestProduct()
        {
            //Product p = new Product();
            //Assert.That(p.Add(5, 3), Is.EqualTo(8), "Sum of the two numbers does not match expected answer");

            Product p = new Product
            {
                Name = "Test",
            };
            Assert.AreEqual("Test", p.Name);
        }   

        [Test]
        public void FetchRequestedPaymentHistory(string email)
        {
            Payment p = new Payment
            {
                BillingName = "James", BillingEmail = "james@uts.com", BillingStreetNum = "5",
                BillingStreet = "A Street", BillingSuburb = "Bankstown", BillingState = "NSW", 
                BillingPostCode = "2200", CreditCardName = "James", CreditCardNumber = "1234123412341234",
            };

            int count = 1;


            Assert.AreEqual("James", p.CreditCardName);
        }
    }
}
