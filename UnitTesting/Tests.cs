﻿using ASDNew.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //David
        //F105: List of Products
        [Test]
        public void TestProductList()
        {
            ASDContext3 db = new ASDContext3();

        }

        //David
        //F106 - Detailed Product Description
        //Also check that no restaurant id displays error page
    }
}
