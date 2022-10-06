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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddRestaurants()
        {
            var controller = DependencyResolver.Current.GetService<RestaurantController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

            List<Restaurant> ExampleRestaurants = new List<Restaurant>();
            Restaurant Restaurant1 = new Restaurant
            {
                Name = "Hungry Jacks",
                Description = "yum yum",
                Email = "contact@hjs.com",
                Password = "hungry123"
            };
            Restaurant Restaurant2 = new Restaurant
            {
                Name = "McDonalds",
                Description = "very yum",
                Email = "info@mcdonalds.com.au",
                Password = "MaccyD"
            };
            ExampleRestaurants.Add(Restaurant1);
            ExampleRestaurants.Add(Restaurant2);
            foreach(Restaurant r in ExampleRestaurants)
            {
                controller.Create(r);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddProducts()
        {
            var controller = DependencyResolver.Current.GetService<ProductController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            if (Rcontroller == null)
                System.Diagnostics.Debug.WriteLine("xd");
            else
                System.Diagnostics.Debug.WriteLine("she'll be right");

            List<Product> ExampleProducts = new List<Product>();
            Product Product1 = new Product
            {
                Name = "Whopper Value Meal",
                Restaurant = Rcontroller.GetRestaurant("Hungry Jacks"),
                Category = new ProductCategory
                {
                    Name = "Burgers"
                },
                Price = 11.65,
                Description = "Comes with 1 whopper burger, large fries and a large Coke. Yum Yum"
            };
            System.Diagnostics.Debug.WriteLine(Product1.Restaurant.Description + "here");
            Product Product2 = new Product
            {
                Name = "Oreo McFlurry",
                Restaurant = Rcontroller.GetRestaurant("McDonalds"),
                Category = new ProductCategory
                {
                    Name = "Ice Cream"
                },
                Price = 4.50,
                Description = "Very yum!!!!"
            };

            ExampleProducts.Add(Product1);
            ExampleProducts.Add(Product2);

            foreach(Product p in ExampleProducts)
            {
                controller.Create(p);
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult AddCustomers()
        {
            return RedirectToAction("Index");
        }

    }
}