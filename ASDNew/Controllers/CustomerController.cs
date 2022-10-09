using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult EditUserDetails()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}