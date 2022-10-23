using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;

namespace ASDNew.Controllers
{
    public class ErrorController : Controller
    {
        ASDContext9 db = new ASDContext9();
        /// <summary>
        /// Display Error Page
        /// </summary>
        /// <returns>Error/Index</returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UnauthorizedMenu(int RestaurantId)
        {
            return View(RestaurantController.GetRestaurant(db, RestaurantId));
        }
    }
}