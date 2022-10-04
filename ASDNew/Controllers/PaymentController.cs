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
        private ASDContext db = new ASDContext();

        // GET: Product
        public ActionResult PaymentHistory()
        {
            //var payments = from r in db.Payments
            //               orderby r.Id
            //               select r;
            //return View(payments);
            return View(db.Payments.ToList());
        }

        public ActionResult PaymentPage()
        {
            return View();
        }

        public ActionResult PaymentSuccess()
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