using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Migrations;
using ASDNew.Models;
using Payment = ASDNew.Models.Payment;
//using static ASDNew.Models.Restaurant;

namespace ASDNew.Controllers
{

    public class PaymentController : Controller
    {
        private ASDContext3 db = new ASDContext3();

        // GET: Product

        public static Payment PaymentHistory(ASDContext3 db, string email)
        {
            List<Payment> paymentHistory = db.Payments.ToList();
            foreach (var item in paymentHistory)
            {
                if (item.BillingEmail.Equals(email))
                {
                    return item;
                }
            }
            return null;
        }

        public ActionResult PaymentHistory(Payment payment)
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