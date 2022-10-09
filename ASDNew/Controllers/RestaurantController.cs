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
        //Database Instance
        private ASDContext5 db = new ASDContext5();

        /// <summary>
        /// Load Restaurant List 
        /// </summary>
        /// <returns>Restaurant/Index View</returns>
        public ActionResult Index()
        {
            //Get all Restaurants in the database
            List<Restaurant> Restaurants = db.Restaurants.ToList();

            //Get ProductCount of restaurants and pass to View
            int[] ProductCounts = GetProductCounts(Restaurants);
            ViewData["ProductCounts"] = ProductCounts;

            //Get Most Common ProductCategories and pass to View
            ProductCategory[] MostCategories = new ProductCategory[Restaurants.Count];
            for (int i = 0; i < Restaurants.Count; i++)
            {
                MostCategories[i] = GetMostSoldCategory(Restaurants[i]);
            }
            ViewData["MostCategory"] = MostCategories;

            //Convert Restaurant List to Array
            Restaurant[] RestaurantArray = Restaurants.ToArray();

            return View(RestaurantArray);
        }

        /// <summary>
        ///  Add new Restaurant to database
        /// </summary>
        /// <param name="Restaurant">Restaurant to add</param>
        /// <returns>Restaurant/Index View</returns>
        public ActionResult Create(Restaurant Restaurant)
        {
            db.Restaurants.Add(Restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get Restaurant from Id
        /// </summary>
        /// <param name="db">Database Instance</param>
        /// <param name="RestaurantId">Restaurant Id</param>
        /// <returns></returns>
        public static Restaurant GetRestaurant(ASDContext5 db, int RestaurantId)
        {
            var restaurant = db.Restaurants
               .AsNoTracking()
               .Where(d => d.Id == RestaurantId)
               .FirstOrDefault();
            
            return restaurant;
        }

        /// <summary>
        /// Get the most sold ProductCategory for a Restaurant
        /// </summary>
        /// <param name="Restaurant">Restaurant to check</param>
        /// <returns>Most sold ProductCategory</returns>
        public ProductCategory GetMostSoldCategory(Restaurant Restaurant)
        {
            List<Product> Products = new List<Product>();
            foreach (Product Product in db.Products.ToList())
            {
                if (Product.Restaurant == Restaurant)
                {
                    Products.Add(Product);
                }
            }

            ProductCategory MostSold = null;
            int BiggestCount = 0;

            foreach (ProductCategory Category in db.ProductCategories.ToList())
            {
                int Count = 0;
                foreach (Product Product in Products)
                {
                    if (Product.Category == Category)
                    {
                        Count++;
                    }
                }
                if (Count > BiggestCount)
                {
                    BiggestCount = Count;
                    MostSold = Category;
                }
            }

            return MostSold;
        }

        /// <summary>
        /// Get the total product count for all restaurants
        /// </summary>
        /// <param name="Restaurants">List of Restaurants</param>
        /// <returns>Array of ProductCounts for each Restaurant</returns>
        public int[] GetProductCounts(List<Restaurant> Restaurants)
        {
            List<Product> AllProducts = db.Products.ToList();
            int[] ProductCounts = new int[Restaurants.Count];
            for (int j = 0; j < Restaurants.Count; j++)
            {
                int Count = 0;
                for (int i = 0; i < AllProducts.Count; i++)
                {
                    if (AllProducts[i].Restaurant == Restaurants[j])
                    {
                        Count++;
                    }
                }
                ProductCounts[j] = Count;
            }
            return ProductCounts;
        }

        /// <summary>
        /// Get Restaurant entry from Database
        /// </summary>
        /// <param name="db">Database Instance</param>
        /// <param name="RestaurantId">Restaurant Id</param>
        /// <returns>Restaurant object</returns>
        // This is a copy of the GetRestaurant(ASDContext5 db, int Id) method with change tracking
        public static Restaurant GetRestaurantForDBOperation(ASDContext5 db, int RestaurantId)
        {
            var restaurant = db.Restaurants
               .Where(d => d.Id == RestaurantId)
               .FirstOrDefault();
            return restaurant;
        }

        /// <summary>
        /// Get Restaurant entry from database by name
        /// </summary>
        /// <param name="db"></param>
        /// <param name="RestaurantName"></param>
        /// <returns>Restaurant object</returns>
        public static Restaurant GetRestaurant(ASDContext5 db, string RestaurantName)
        {
            var Restaurants = from r in db.Restaurants
                              select r;
            foreach (var Restaurant in Restaurants)
            {
                if (Restaurant.Name.Equals(RestaurantName))
                {
                    return Restaurant;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Get Database instance
        /// </summary>
        /// <returns>Instance of database</returns>
        public static ASDContext5 GetDatabase()
        {
            return new ASDContext5();
        }
    }
}