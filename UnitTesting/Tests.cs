using ASDNew.Controllers;
using ASDNew.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        ASDContext9 db = new ASDContext9();

        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        //Patrick.E
        //F109: Adding products to restaurant
        /*
        [Test]
        public void TestAddProduct()
        {
            Random r = new Random();

            // Initialise database
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            // Get product count from database before adding the new product
            int prodCountBefore = db.Products.Count();

            // Create a new product to test
            int restaurantId = db.Restaurants.First().Id;
            int prodCategory = db.ProductCategories.First().Id;
            string prodName = "UNIT TEST Delicious Cheeseburger";
            double prodPrice = Math.Round(r.NextDouble() * 10, 2);
            string prodDescription = "This is a tasty cheeseburger. Some more sample text!";

            // Trigger the function in the Controller class
            ProductController controller = new ProductController();
            controller.Create(restaurantId, prodCategory, prodName, prodPrice, prodDescription);

            // Query the database after adding the new product
            ASDContext9 dbReplica = new ASDContext9();
            Product retrievedProduct = dbReplica.Products.OrderByDescending(p => p.Id).Include(x => x.Category).Include(y => y.Restaurant).FirstOrDefault();
            int prodCountAfter = dbReplica.Products.Count();

            // Perform checks
            Assert.NotNull(retrievedProduct);
            Assert.IsTrue(prodCountAfter == (prodCountBefore + 1), "Number of product records was expected to increase by 1 after adding new product");
            Assert.AreEqual(restaurantId, retrievedProduct.Restaurant.Id);
            Assert.AreEqual(prodCategory, retrievedProduct.Category.Id);
            Assert.AreEqual(prodName, retrievedProduct.Name);
            Assert.AreEqual(prodPrice, retrievedProduct.Price);
            Assert.AreEqual(prodDescription, retrievedProduct.Description);
        }
        */

        //Patrick.E
        //F110: Edit/delete products in restaurant
        /*
        [Test]
        public void TestEditProduct()
        {
            Random r = new Random();

            // Initialise database
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            // Get random product record to update
            int total = db.Products.Count();
            int offset = r.Next(0, total);
            Product productToUpdate = db.Products.OrderBy(p => p.Id).Include(x => x.Category).Include(y => y.Restaurant).Skip(offset).FirstOrDefault();

            // Get random category
            int categoryTotal = db.ProductCategories.Count();
            int categoryOffset = r.Next(0, categoryTotal);
            ProductCategory randomCategory = db.ProductCategories.OrderBy(c => c.Id).Skip(categoryOffset).FirstOrDefault();

            // Set new properties
            int newCategory = randomCategory.Id;
            string newName = "UNIT TEST Name update";
            double newPrice = Math.Round(r.NextDouble() * 10, 2);
            string newDescription = "Testing the description...";

            // Trigger the edit function
            ProductController controller = new ProductController();
            controller.Edit(productToUpdate.Id, productToUpdate.Restaurant.Id, newCategory, newName, newPrice, newDescription);

            // Retrieve from database after editing the product
            Product retrievedProduct = db.Products.Find(productToUpdate.Id);
            db.Entry(retrievedProduct).Reload();

            // Perform checks
            Assert.NotNull(retrievedProduct);
            Assert.AreEqual(productToUpdate.Restaurant.Id, retrievedProduct.Restaurant.Id);
            Assert.AreEqual(newCategory, retrievedProduct.Category.Id);
            Assert.AreEqual(newName, retrievedProduct.Name);
            Assert.AreEqual(newPrice, retrievedProduct.Price);
            Assert.AreEqual(newDescription, retrievedProduct.Description);
        }
        */

        //Patrick.E
        //F110: Edit/delete products in restaurant
        /*
        [Test]
        public void TestDeleteProduct()
        {
            Random r = new Random();

            // Initialise database
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            // Get product count from database before deleting the product
            int prodCountBefore = db.Products.Count();

            // Get random product record to update
            int total = db.Products.Count();
            int offset = r.Next(0, total);
            Product productToUpdate = db.Products.OrderBy(p => p.Id).Include(x => x.Category).Include(y => y.Restaurant).Skip(offset).FirstOrDefault();

            // Trigger the delete function
            ProductController controller = new ProductController();
            controller.Delete(productToUpdate.Id, productToUpdate.Restaurant.Id);

            // Try to retrieve from database after deleting the product
            ASDContext9 dbReplica = new ASDContext9();
            Product retrievedProduct = dbReplica.Products.Find(productToUpdate.Id);
            int prodCountAfter = dbReplica.Products.Count();

            // Perform checks
            Assert.Null(retrievedProduct, "Retrieved product was expected to be null");
            Assert.IsTrue(prodCountAfter == (prodCountBefore - 1), "Number of product records was expected to decrease by 1 after deleting the product");
        }
        */

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
        /*
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
        */

        /*
        [Test]
        public void TestProductCount()
        {
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            Restaurant McDonalds = RestaurantController.GetRestaurant(db, "McDonalds");

            int ProductCount = 0;
            foreach (Product Product in db.Products.ToList())
            {
                if (Product.Restaurant == McDonalds)
                {
                    ProductCount++;
                }
            }

            //Assert.That(ProductCount >= 8);
            //Assert.That(ProductCount <= 16);
        }
        */

        /*
        [Test]
        public void TestCategoryDuplication()
        {
            AdminController AdminController = new AdminController();
            AdminController.AddProductCategories();
            AdminController.AddRestaurants();
            AdminController.AddProducts();

            Assert.AreEqual(db.ProductCategories.ToList().Count, db.ProductCategories.Distinct().ToList().Count);
        }
        */

        //[Test]
        //public void TestAddRestaurant()
        //{
        //    Random r = new Random();

        //    // Initialise database
        //    AdminController AdminController = new AdminController();
        //    AdminController.AddRestaurants();

        //    // Get product count from database before adding the new product
        //    int restCountBefore = db.Restaurants.Count();

        //    // Create a new product to test
        //    string RestaurantName = "Fatmans Steakhouse";
        //    string RestaurantDescription = "Best chicken Snitty";
        //    string RestaurantEmail = "Fatmans@gmail.com";
        //    string RestaurantPassword = "Nice123";

        //    // Trigger the function in the Controller class
        //    RestaurantController controller = new RestaurantController();
        //    controller.Create(RestaurantName, RestaurantDescription, RestaurantEmail, RestaurantPassword);

        //    // Query the database after adding the new product
        //    ASDContext9 dbReplica = new ASDContext9();
        //    Restaurant retrievedRestaurant = dbReplica.Restaurants.OrderByDescending(p => p.Id).FirstOrDefault();
        //    int restCountAfter = dbReplica.Products.Count();

        //    // Perform checks
        //    Assert.NotNull(retrievedRestaurant);
        //    Assert.IsTrue(restCountAfter == (restCountBefore + 1), "Number of restaurant records was expected to increase by 1 after adding new restaurant");
        //    Assert.AreEqual(RestaurantName, retrievedRestaurant.Name);
        //    Assert.AreEqual(RestaurantDescription, retrievedRestaurant.Description);
        //    Assert.AreEqual(RestaurantEmail, retrievedRestaurant.Email);
        //    Assert.AreEqual(RestaurantPassword, retrievedRestaurant.Password);
        //}

        //[Test]
        //public void TestEditRestaurant()
        //{
        //    Random r = new Random();

        //    // Initialise database
        //    AdminController AdminController = new AdminController();
        //    AdminController.AddRestaurants();

        //    // Get random product record to update
        //    int total = db.Restaurants.Count();
        //    int offset = r.Next(0, total);
        //    Restaurant restaurantToUpdate = db.Restaurants.OrderBy(p => p.Id).FirstOrDefault();

        //    // Set new properties
        //    string RestaurantName = "Fatmans Steakhouse";
        //    string RestaurantDescription = "Best chicken Snitty";
        //    string RestaurantEmail = "Fatmans@gmail.com";
        //    string RestaurantPassword = "Nice123";

        //    // Trigger the edit function
        //    RestaurantController controller = new RestaurantController();
        //    controller.Edit(RestaurantName, RestaurantDescription, RestaurantEmail, RestaurantPassword);

        //    // Retrieve from database after editing the product
        //    Restaurant retrievedRestaurant = db.Restaurants.Find(restaurantToUpdate.Id);
        //    db.Entry(retrievedRestaurant).Reload();

        //    // Perform checks
        //    Assert.NotNull(retrievedRestaurant);
        //    Assert.AreEqual(RestaurantName, retrievedRestaurant.Name);
        //    Assert.AreEqual(RestaurantDescription, retrievedRestaurant.Description);
        //    Assert.AreEqual(RestaurantEmail, retrievedRestaurant.Email);
        //    Assert.AreEqual(RestaurantPassword, retrievedRestaurant.Password);
        //}

        //[Test]
        //public void TestDeleteRestaurant()
        //{

        //    Random r = new Random();

        //    // Initialise database

        //    SqlConnection cnn;
        //    cnn = new SqlConnection();
        //    Random r = new Random();

        //      // Initialise database

        //    AdminController AdminController = new AdminController();
        //    AdminController.AddProductCategories();
        //    AdminController.AddRestaurants();
        //    AdminController.AddProducts();

        //    // Get product count from database before deleting the product
        //    int prodCountBefore = db.Restaurants.Count();

        //    // Get random product record to update
        //    int total = db.Restaurants.Count();
        //    int offset = r.Next(0, total);
        //    Restaurant restaurantToUpdate = db.Restaurants.OrderBy(a => a.Id).Skip(offset).FirstOrDefault();

        //    // Get restaurant count from database before deleting the restaurant
        //    int restCountBefore = db.Restaurants.Count();

        //    // Get random restaurant to delete
        //    int total = db.Restaurants.Count();
        //    int offset = r.Next(1, total);
        //    Restaurant restaurantToUpdate = db.Restaurants.FirstOrDefault(a => a.Id == offset);

        //    // Trigger the delete function
        //    RestaurantController controller = new RestaurantController();
        //    controller.Delete(restaurantToUpdate.Id);


        //    // Try to retrieve from database after deleting the product

        //    // Try to retrieve from database after deleting the restaurant

        //    ASDContext9 dbReplica = new ASDContext9();
        //    Restaurant retrievedRestaurant = dbReplica.Restaurants.Find(restaurantToUpdate.Id);
        //    int restCountAfter = dbReplica.Restaurants.Count();

        //    // Perform checks
        //    Assert.Null(retrievedRestaurant, "Retrieved restaurant was expected to be null");

        //    Assert.IsTrue(restCountAfter == (prodCountBefore - 1), "Number of Restaurant records was expected to decrease by 1 after deleting the Restaurant");

        //    Assert.IsTrue(restCountAfter == (restCountBefore - 1), "Number of Restaurant records was expected to decrease by 1 after deleting the Restaurant");

        //}

    }
}
