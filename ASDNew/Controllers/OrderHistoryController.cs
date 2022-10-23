using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;

namespace ASDNew.Controllers
{
    public class OrderHistoryController : Controller
    {
        private ASDContext9 db = new ASDContext9();
        
        // GET: OrderHistory
        public ActionResult Index()
        {

            List<OrderHistory> Restaurants = db.OrderHistory.ToList();
            List<string> data = new List<string>();
            foreach (var item in Restaurants.Select((value, i) => new { i, value }))
            {
                var value = item.value;
                var index = item.i;
                data.Add(value.OrderId.ToString());
                ViewBag.orderid =data;
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult OrderDetails()
        {
            List<OrderHistory> Restaurants = db.OrderHistory.ToList();
           
            return Json(Restaurants, JsonRequestBehavior.AllowGet);
        }
    }
}