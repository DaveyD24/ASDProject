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

    public class RestaurantController : Controller
    {
        private ASDContext db = new ASDContext();

        // GET: Product
        public ActionResult Index()
        {
            var restaurants = from r in db.Restaurants
                           orderby r.Id
                           select r;
            return View(restaurants);
        }

        public ActionResult ProductPage()
        {
            return View();
        }

        public ActionResult Create(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public Restaurant GetRestaurant(int Id)
        {
            //var restaurants = from r in db.Restaurants
            //                  select r;
            //foreach (var r in restaurants)
            //{
            //    if (r.Id == Id)
            //    {
            //        return r;
            //    }
            //}
            //return null;

            var restaurant = db.Restaurants
               .AsNoTracking()
               .Where(d => d.Id == Id)
               .FirstOrDefault();
            return restaurant;

        }

        public Restaurant GetRestaurant(string Name)
        {
            var restaurants = from r in db.Restaurants
                              select r;
            foreach (var r in restaurants)
            {
                if (r.Description.Equals(Name))
                {
                    return r;
                }
            }
            return null;
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}