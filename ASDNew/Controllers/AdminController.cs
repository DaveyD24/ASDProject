using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;

namespace ASDNew.Controllers
{
    public class AdminController : Controller
    {
        ASDContext5 db = new ASDContext5();

        /// <summary>
        /// Load Admin Control Panel
        /// </summary>
        /// <returns>Admin/Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Adds ProductCategories from SampleData to the database
        /// </summary>
        /// <returns>Redirect to Admin Control Panel</returns>
        public ActionResult AddProductCategories()
        {
            SampleProductCategory SampleProductCategories = new SampleProductCategory();
            foreach (ProductCategory Category in SampleProductCategories.SampleCategories)
            {
                if (!ContainsCategory(Category))
                {
                    db.ProductCategories.Add(Category);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Adds Restaurants from SampleData to the database
        /// </summary>
        /// <returns>Redirect to Admin Control Panel</returns>
        public ActionResult AddRestaurants()
        {
            SampleRestaurant SampleRestaurants = new SampleRestaurant();
            foreach (Restaurant Restaurant in SampleRestaurants.AllRestaurants)
            {
                db.Restaurants.Add(Restaurant);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Check if a category has already been added to the database
        /// </summary>
        /// <param name="Category">Category to check</param>
        /// <returns>whether Category exists in database</returns>
        public bool ContainsCategory(ProductCategory Category)
        {
            List<ProductCategory> ProductCategories = db.ProductCategories.ToList();
            foreach (ProductCategory ProductCategory in ProductCategories)
            {
                if (ProductCategory.Name.Equals(Category.Name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add Products from SampleData to the database
        /// </summary>
        /// <returns>Redirect to Admin Control Panel</returns>
        public ActionResult AddProducts()
        {
            SampleProduct SampleProducts = new SampleProduct();
            List<Restaurant> AllRestaurantsInDb = db.Restaurants.ToList();

            int ProductCount = 12;

            foreach (Restaurant Restaurant in AllRestaurantsInDb)
            {
                Random Random = new Random();
                List<Product> AllProducts = SampleProducts.AllProducts;
                var Indices = Enumerable.Range(0, ProductCount).OrderBy(g => Random.Next()).ToList();

                for (int i = 0; i < Indices.Count; i++)
                {
                    Product Product = AllProducts[i];
                    Product.Restaurant = Restaurant;
                    db.Products.Add(Product);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddCustomers()
        {
            return RedirectToAction("Index");
        }

    }
}