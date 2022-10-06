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

    public class PaymentController : Controller
    {
        private ASDContext2 db = new ASDContext2();

        // GET: Product
        public ActionResult Index()
        {
            var payments = from r in db.Payments
                         orderby r.Id
                         select r;
            return View(payments);
        }

        public ActionResult ProductPage()
        {
            return View();
        }

        public ActionResult Create(Payment payment)
        {
            db.Payments.Add(payment);
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