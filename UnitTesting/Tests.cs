﻿using ASDNew.Controllers;
using ASDNew.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

namespace UnitTesting
{

    [TestFixture]
    public class Tests
    {

        ASDContext5 db = new ASDContext5();

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
        public void TestAddProduct()
        {
            // Get product count from database before adding the new product
            int prodCountBefore = db.Products.Count();

            // Create a new product to test
            int restaurantId = db.Restaurants.First().Id;
            int prodCategory = db.ProductCategories.First().Id;
            string prodName = "UNIT TEST Delicious Cheeseburger";
            double prodPrice = 5.95;
            string prodDescription = "This is a tasty cheeseburger. Some more sample text!";

            ProductController controller = new ProductController();

            // Trigger the function in the Controller class
            ActionResult actionResult = controller.Create(restaurantId, prodCategory, prodName, prodPrice, prodDescription);

            // Query the database after adding the new product
            Product retrievedProduct = db.Products.OrderByDescending(p => p.Id).FirstOrDefault();
            int prodCountAfter = db.Products.Count();

            // Assert statements
            Assert.NotNull(retrievedProduct);
            Assert.IsTrue(prodCountAfter == (prodCountBefore + 1), "Number of product records was expected to increase by 1 after adding new product");
            Assert.AreEqual(restaurantId, retrievedProduct.Restaurant.Id);
            Assert.AreEqual(prodCategory, retrievedProduct.Category.Id);
            Assert.AreEqual(prodName, retrievedProduct.Name);
            Assert.AreEqual(prodPrice, retrievedProduct.Price);
            Assert.AreEqual(prodDescription, retrievedProduct.Description);
        }

        [Test] //David
        public void TestStringConverter()
        {
            SampleRestaurant sr = new SampleRestaurant();
            string testString = "I Want The Whitespace Removed From This";
            testString = sr.RemoveWhitespace(testString);

            Assert.AreEqual("IWantTheWhitespaceRemovedFromThis", testString);
        }

        [Test] //David
        public void TestErrorPage()
        {
            ProductController PC = new ProductController();
            ViewResult PageReturn = (ViewResult)PC.Index(null);
            Assert.AreEqual("~/Views/Error/Index.cshtml", PageReturn.ViewName);
        }

        //Brendan
        //F112: Show payment history
        [Test]
        public void ShowPaymentHistory()
        {
            //Arrange new payment
            Payment newPayment = new Payment
            {
                BillingName = "James",
                BillingEmail = "james@uts.com",
            };

            //Act - add to database and if there are records with same email, store them in payments
            Payment payments = PaymentController.PaymentHistory(db, newPayment.BillingEmail);

            //Assert that payments is not null and that there are payment records in the payment database
            Assert.NotNull(payments);
        }

        [Test]
        public void TestProductCount()
        {
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            foreach (Restaurant Restaurant in db.Restaurants.ToList())
            {
                int ProductCount = 0;
                foreach (Product Product in db.Products.ToList())
                {
                    if (Product.Restaurant == Restaurant)
                    {
                        ProductCount++;
                    }
                }
                Assert.That(ProductCount >= 8);
                Assert.That(ProductCount <= 16);
            }
        }

        [Test]
        public void TestCategoryDuplication()
        {
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            Assert.AreEqual(db.ProductCategories.ToList().Count, db.ProductCategories.Distinct().ToList().Count);
        }

    }
}
