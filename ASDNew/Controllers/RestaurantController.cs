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
        private ASDContext3 db = new ASDContext3();

        // GET: Product
        public ActionResult Index()
        {
            var restaurants = from r in db.Restaurants
                           orderby r.Id
                           select r;
            return View(restaurants);
        }

        public ActionResult ListOfRestaurants()
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

        public static Restaurant GetRestaurant(ASDContext3 db, int Id)
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

        // This is a copy of the GetRestaurant(ASDContext3 db, int Id) method with change tracking
        public static Restaurant GetRestaurantForDBOperation(ASDContext3 db, int Id)
        {
            var restaurant = db.Restaurants
               .Where(d => d.Id == Id)
               .FirstOrDefault();
            return restaurant;
        }

        public static Restaurant GetRestaurant(ASDContext3 db, string Name)
        {
            var restaurants = from r in db.Restaurants
                              select r;
            foreach (var r in restaurants)
            {
                if (r.Name.Equals(Name))
                {
                    return r;
                }
            }
            return null;
        }

        public static ASDContext3 GetDatabase()
        {
            return new ASDContext3();
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}