// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using ASDxd.Model;
using System.Collections;
using System.Collections.Generic;

namespace ASDxd.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test]
        public void TestProduct()
        {
            Product p = new Product();
            Assert.That(p.Add(5, 3), Is.EqualTo(8), "Sum of the two numbers does not match expected answer");
        }
    }
}
