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

    public class OrderController : Controller
    {
        private ASDContext2 db = new ASDContext2();

        // GET: Product
        public ActionResult Index()
        {
            var orders = from r in db.Orders
                              orderby r.Id
                              select r;
            return View(orders);
        }

        public ActionResult ProductPage()
        {
            return View();
        }

        public ActionResult Create(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}