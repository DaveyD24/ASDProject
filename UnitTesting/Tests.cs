﻿using ASDNew.Controllers;
using ASDNew.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using ASDNew.Migrations;
using Payment = ASDNew.Models.Payment;

namespace UnitTesting
{

    [TestFixture]
    public class Tests
    {

        ASDContext3 db = new ASDContext3();

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

        //Brendan
        //F112: Show payment history
        [Test]
        public void ShowPaymentHistory()
        {
            //Arrange new payment
            Payment newPayment = new Payment
            {
                BillingName = "James", BillingEmail = "james@uts.com",
            };

            //Act - add to database and if there are records with same email, store them in payments
            Payment payments = PaymentController.PaymentHistory(db, newPayment.BillingEmail);

            //Assert that payments is not null and that there are payment records in the payment database
            Assert.NotNull(payments);
        }

        //David
        //F105: List of Products
        [Test]
        public void TestProductList()
        {
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, 7);
            List<Product> AllProducts = ProductController.GetAllProducts(db);
            List<Product> FilteredProducts = ProductController.FilterProductList(Restaurant, AllProducts);
            List<ProductCategory> Categories = ProductController.GetRelevantCategories(FilteredProducts);

            Assert.That(Categories.Contains(ProductController.GetCategory(db, "Burgers")));
            Assert.That(Categories.Count == 1);

        }

        [Test]
        public void TestStringConverter()
        {
            SampleRestaurant sr = new SampleRestaurant();
            string testString = "I Want The Whitespace Removed From This";
            testString = sr.RemoveWhitespace(testString);

            Assert.AreEqual("IWantTheWhitespaceRemovedFromThis", testString);
        }

        //David
        //F106 - Detailed Product Description
        //Also check that no restaurant id displays error page
    }
}
