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
        private ASDContext5 db = new ASDContext5();

        // GET: Product
        public ActionResult Index()
        {
            var restaurants = from r in db.Customers
                              orderby r.Id
                              select r;
            return View(restaurants);
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult EditUserDetails(int Id)
        {
            if (Session["Username"] != null)
            {
                var user = db.Customers.Find(Id); 
            }
            return View("EditUserDetails", "Customer", new {UserId = Id});
        }

        public ActionResult Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            var usr = db.Customers.Single(c => c.Email == customer.Email && c.Password == customer.Password);
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View() ;
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