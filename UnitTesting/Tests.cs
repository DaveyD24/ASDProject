using ASDNew.Controllers;
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

    }
}
