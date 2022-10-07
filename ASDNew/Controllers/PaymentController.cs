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
        private ASDContext3 db = new ASDContext3();

        // GET: Product
        public ActionResult PaymentHistory()
        {
            return View(db.Payments.ToList());
        }

        public ActionResult PaymentPage()
        {
            return View();
        }

        public ActionResult PaymentSuccess(Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.Date = DateTime.Now;
                db.Payments.Add(payment);
                db.SaveChanges();
                return View(db.Payments.ToList());
            }
            return View("PaymentPage",payment);
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}