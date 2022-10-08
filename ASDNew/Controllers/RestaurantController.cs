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
            List<Restaurant> restaurants = db.Restaurants.ToList();

            int[] productCounts = GetProductCounts(restaurants);
            ViewData["productCounts"] = productCounts;

            ProductCategory[] mostCategories = new ProductCategory[restaurants.Count];

            for (int i = 0; i < restaurants.Count; i++)
            {
                mostCategories[i] = GetMostSoldCategory(restaurants[i]);
            }
            ViewData["mostCategory"] = mostCategories;

            Restaurant[] RestaurantArray = restaurants.ToArray();

            return View(RestaurantArray);
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
            var restaurant = db.Restaurants
               .AsNoTracking()
               .Where(d => d.Id == Id)
               .FirstOrDefault();
            return restaurant;

        }

        public ProductCategory GetMostSoldCategory(Restaurant Restaurant)
        {
            List<Product> Products = new List<Product>();
            foreach (Product p in db.Products.ToList())
            {
                if (p.Restaurant == Restaurant)
                {
                    Products.Add(p);
                }
            }

            ProductCategory MostSold = null;
            int biggestCount = 0;

            foreach (ProductCategory pc in db.ProductCategories.ToList())
            {
                int count = 0;
                foreach (Product p in Products)
                {
                    if (p.Category == pc)
                    {
                        count++;
                    }
                }
                if (count > biggestCount)
                {
                    biggestCount = count;
                    MostSold = pc;
                }
            }

            return MostSold;
        }

        public int[] GetProductCounts(List<Restaurant> restaurants)
        {
            List<Product> AllProduct = db.Products.ToList();
            int[] ProductCounts = new int[restaurants.Count];
            for (int j = 0; j < restaurants.Count; j++)
            {
                int count = 0;
                for (int i = 0; i < AllProduct.Count; i++)
                {
                    if (AllProduct[i].Restaurant == restaurants[j])
                    {
                        count++;
                    }
                }
                ProductCounts[j] = count;
            }
            return ProductCounts;
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