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
        private ASDContext9 db = new ASDContext9();

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
        /// Load Restaurant List for admin use
        /// </summary>
        /// <returns>Restaurant/listOfRestaurants View</returns>
        public ActionResult listOfRestaurants()
        {
            //Get all Restaurants in the database
            List<Restaurant> Restaurants = db.Restaurants.ToList();

            //Convert Restaurant List to Array
            Restaurant[] RestaurantArray = Restaurants.ToArray();

            return View(RestaurantArray);
        }

        /// <summary>
        /// Get Restaurant from Id
        /// </summary>
        /// <param name="db">Database Instance</param>
        /// <param name="RestaurantId">Restaurant Id</param>
        /// <returns></returns>
        public static Restaurant GetRestaurant(ASDContext9 db, int RestaurantId)
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
        // This is a copy of the GetRestaurant(ASDContext9 db, int Id) method with change tracking
        public static Restaurant GetRestaurantForDBOperation(ASDContext9 db, int RestaurantId)
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
        public static Restaurant GetRestaurant(ASDContext9 db, string RestaurantName)
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
        /// Load Add Restaurant Page
        /// </summary>
        /// <returns>Restaurant/AddRestaurant View</returns>
        public ActionResult AddRestaurant()
        {

            return View();
        }

        /// <summary>
        /// Load Edit Restaurant Page
        /// </summary>
        /// <returns>Restaurant/EditRestaurant View</returns>
        public ActionResult EditRestaurant(int? RestaurantId)
        {
            // Show error page if parameters are null
            if (RestaurantId == null)
            {
                System.Diagnostics.Debug.WriteLine("One or more parameters is null");
                return View("Error");
            }

            // Store data to be used by the View
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;

            // Show error page if restaurant ID does not exist
            if (Restaurant == null)
            {
                System.Diagnostics.Debug.WriteLine("Restaurant and/or product is null or could not be found");
                return View("Error");
            }

            return View(Restaurant);
        }

        /// <summary>
        /// Load Delete Restaurant Page
        /// </summary>
        /// <returns>Restaurant/DeleteRestaurant View</returns>
        public ActionResult DeleteRestaurant(int? RestaurantId)
        {
            // Show error page if parameters are null
            if (RestaurantId == null)
            {
                System.Diagnostics.Debug.WriteLine("Parameter is null");
                return View("Error");
            }

            // Store data to be used by the View
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;

            // Show error page if restaurant ID does not exist
            if (Restaurant == null)
            {
                System.Diagnostics.Debug.WriteLine("Restaurant is null or could not be found");
                return View("Error");
            }

            return View(Restaurant);
        }

        /// <summary>
        ///  Add new Restaurant to database
        /// </summary>
        /// <param name="Restaurant">Restaurant to add</param>
        /// <returns>Restaurant/listOfRestaurant View</returns>
        public ActionResult Create(Restaurant Restaurant)
        {
            ///adds restaurant to db
            db.Restaurants.Add(Restaurant);
            SampleProduct SampProd = new SampleProduct();
            Random random = new Random();
            int ProductCount = random.Next(8, 16); ;
            for (int i = 0; i < ProductCount; i++)
            {
                Product prod = SampProd.GetRandomProduct();
                prod.Restaurant = Restaurant;
                db.Products.Add(prod);
            }
            ///saves changes
            db.SaveChanges();
            ///returns to admin page
            return RedirectToAction("Index", "Admin");

        }

        /// <summary>
        ///  Edits selected restaurant
        /// </summary>
        /// <param name="Restaurant">Restaurant to edit</param>
        /// <returns>Restaurant/listOf View</returns>
        public ActionResult Edit(int RestaurantId, string RestaurantName, string RestaurantDescription, string RestaurantEmail, string RestaurantPassword)
        {
            // Retrieve existing restaurant from database
            Restaurant Entity = db.Restaurants.FirstOrDefault(rest => rest.Id == RestaurantId);

            // Check restaurant ID exists
            if (Entity != null)
            {
                // Update restaurant with new details
                Entity.Name = RestaurantName;
                Entity.Description = RestaurantDescription;
                Entity.Email = RestaurantEmail;
                Entity.Password = RestaurantPassword;

                try
                {
                    // Save changes to database
                    db.SaveChanges();

                    // Redirect user to list of restaurant page
                    return RedirectToAction("Index");
                }
                catch (Exception E)
                {
                    System.Diagnostics.Debug.WriteLine(E.Message);
                    System.Diagnostics.Debug.WriteLine(E.StackTrace);
                    return View("Error");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Edit Restaurant entity is null");
                return View("Error");
            }
        }

            /// <summary>
            ///  Delete selected Restaurant to database
            /// </summary>
            /// <param name="Restaurant">Restaurant to delete</param>
            /// <returns>Restaurant/listOfRestaurants View</returns>
            public ActionResult Delete(int RestaurantId)
            {
                // Fetch restaurant ID from database
                Restaurant Entity = db.Restaurants.FirstOrDefault(rest => rest.Id == RestaurantId);
                List<Product> AllProducts = ProductController.GetAllProducts(db);
                List<Product> FilteredProducts = ProductController.FilterProductList(Entity, AllProducts);

                // Check restaurant ID exists then deletes restaurant
                // If the restaurant can't be deleted throws error
                if (Entity != null)
                {
                    try
                    {
                        foreach (Product Product in FilteredProducts)
                        {
                            db.Products.Remove(Product);
                        }
                        // Remove restaurant record from database
                        db.Restaurants.Remove(Entity);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception E)
                    {
                        System.Diagnostics.Debug.WriteLine(E.Message);
                        System.Diagnostics.Debug.WriteLine(E.StackTrace);
                        return View("Error");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DeleteRestaurant entity is null");
                    return View("Error");
                }
            }

            /// <summary>
            /// Get Database instance
            /// </summary>
            /// <returns>Instance of database</returns>
            public static ASDContext9 GetDatabase()
            {
                return new ASDContext9();
            }
        }
    }