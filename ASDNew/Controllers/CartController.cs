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

    public class CartController : Controller
    {
        private ASDContext3 db = new ASDContext3();

        // GET: Product

        public ActionResult CartPage()
        {
            return View();
        }

        public ActionResult AddtoCart()
        {
            return View(db.Carts.ToList());
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}