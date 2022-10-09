using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASDNew.Controllers
{
    public class ErrorController : Controller
    {

        /// <summary>
        /// Display Error Page
        /// </summary>
        /// <returns>Error/Index</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}