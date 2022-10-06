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

        // GET: Product
        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                System.Diagnostics.Debug.WriteLine("xdddddddddd");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(RestaurantID);
            }
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);
            ViewData["Restaurant"] = Rcontroller.GetRestaurant((int)RestaurantID);
            //var products = from p in db.Products
            //               orderby p.Id
            //               select p;
            var products = db.Products
                .Include(a => a.Category)
                .Include(a => a.Restaurant)
                .ToList();

            List<Product> newList = new List<Product>();
            foreach (var p in products)
            {
                if (p.Restaurant.Id == RestaurantID)
                {
                    newList.Add(p);
                }
            }
            List<ProductCategory> categories = new List<ProductCategory>();
            foreach (var p in newList)
            {
                if (!categories.Contains(p.Category))
                {
                    categories.Add(p.Category);
                }
            }
            ViewData["Categories"] = categories;

            return View(newList);
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