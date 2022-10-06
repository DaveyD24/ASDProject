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

    public class ProductController : Controller
    {
        private ASDContext3 db = new ASDContext3();

        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                System.Diagnostics.Debug.WriteLine("xdddddddddd");
                //Display Error Page
            }

            
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);
            
            Restaurant Restaurant = Rcontroller.GetRestaurant((int)RestaurantID);
            ViewData["Restaurant"] = Restaurant;

            List<Product> AllProducts = GetAllProducts();
            List<Product> FilteredProducts = FilterProductList(Restaurant, AllProducts);
            List<ProductCategory> RelevantCategories = GetRelevantCategories(FilteredProducts);

            ViewData["Categories"] = RelevantCategories;

            return View(FilteredProducts);
        }

        public List<Product> GetAllProducts()
        {
            List<Product> AllProducts = new List<Product>();
            AllProducts = db.Products
                .Include(a => a.Category)
                .Include(a => a.Restaurant)
                .ToList();
            return AllProducts;
        }

        public List<Product> FilterProductList(Restaurant Restaurant, List<Product> Products)
        {
            List<Product> FilteredProducts = new List<Product>();
            foreach (var p in Products)
            {
                if (p.Restaurant.Id == Restaurant.Id)
                {
                    FilteredProducts.Add(p);
                }
            }
            return FilteredProducts;
        }

        public List<ProductCategory> GetRelevantCategories(List<Product> Products)
        {
            List<ProductCategory> RelevantCategories = new List<ProductCategory>();
            foreach (var p in Products)
            {
                if (!RelevantCategories.Contains(p.Category))
                {
                    RelevantCategories.Add(p.Category);
                }
            }
            return RelevantCategories;
        }

        public void AddToCart(string test)
        {
            System.Diagnostics.Debug.WriteLine(test);
        }

        public ActionResult ProductPage()
        {
            return View();
        }
        
        public ActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddProduct()
        {
            List<ProductCategory> categories = db.ProductCategories.ToList();
            return View(categories);
        }

        [HttpPost]
        public ActionResult Create(Restaurant restaurant, string prodCategory, string prodName, double prodPrice, string prodDesc)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            

            List<ProductCategory> Cats = db.ProductCategories.ToList();
            ProductCategory Cat = null;
            foreach(var c in Cats)
            {
                if (c.Name.Equals(prodCategory))
                {
                    Cat = c;
                }
            }

            Product product = new Product
            {
                Restaurant = Rcontroller.GetRestaurant(5),
                Category = Cat,
                Name = prodName,
                Price = prodPrice,
                Description = prodDesc
            };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Edit()
        //{

        //}

        //public ActionResult Delete()
        //{

        //}

    }
}