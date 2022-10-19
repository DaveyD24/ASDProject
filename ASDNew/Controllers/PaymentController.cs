using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;
using Payment = ASDNew.Models.Payment;
//using static ASDNew.Models.Restaurant;

namespace ASDNew.Controllers
{

    public class PaymentController : Controller
    {
        //Instance of Database
        private ASDContext8 db = new ASDContext8();

        /// <summary>
        /// Get Payment from database for user
        /// </summary>
        /// <param name="db">Database instance</param>
        /// <param name="Email">Email of user</param>
        /// <returns>Payment object for user</returns>
        public static Payment PaymentHistory(ASDContext8 db, string Email)
        {
            List<Payment> PaymentHistory = db.Payments.ToList();
            foreach (Payment Payment in PaymentHistory)
            {
                if (Payment.BillingEmail.Equals(Email))
                {
                    return Payment;
                }
            }
            return null;
        }

        /// <summary>
        /// Displays Payment History page for user
        /// </summary>
        /// <param name="Payment">Payment object</param>
        /// <returns>Payment/PaymentHistory View</returns>
        public ActionResult PaymentHistory(Payment Payment)
        {
            return View(db.Payments.ToList());
        }
        
        /// <summary>
        /// Displays Payment Page
        /// </summary>
        /// <returns>Payment/PaymentPage View</returns>
        public ActionResult PaymentPage()
        {
            return View();
        }

        /// <summary>
        /// Adds successful payment to the database
        /// </summary>
        /// <param name="Payment">Payment to add</param>
        /// <returns>Payment list or redirect to PaymentPage with existing payment</returns>
        public ActionResult PaymentSuccess(Payment Payment)
        {
            if (ModelState.IsValid)
            {
                Payment.Date = DateTime.Now;
                db.Payments.Add(Payment);
                db.SaveChanges();
                return View(db.Payments.ToList());
            }
            return View("PaymentPage", Payment);
        }
    }
}