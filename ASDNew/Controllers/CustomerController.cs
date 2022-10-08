using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;
//using static ASDNew.Models.Restaurant;

namespace ASDNew.Controllers
{

    public class CustomerController : Controller
    {
        private ASDContext3 db = new ASDContext3();

        // GET: Product
        public ActionResult Index()
        {
            var restaurants = from r in db.Customers
                              orderby r.Id
                              select r;
            return View(restaurants);
        }

        public ActionResult EditUserDetails(int userId)
        {
            if (Session["Username"] != null)
            {
                var user = db.Customers.Find(userId); 
            }
            return View("EditUserDetails", "Customer", new {Id = userId});
        }

        public ActionResult Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int userId, string FirstName, string LastName, string Email, string Password)
        {
            if (Session["Username"] != null)
            {

                return RedirectToAction("ShowUpdatedDetails");
            }
            return View();
        }

        //public ActionResult Delete()
        //{

        //}

    }
}