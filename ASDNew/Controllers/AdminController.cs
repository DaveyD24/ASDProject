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
        ASDContext3 db = new ASDContext3();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProductCategories()
        {
            SampleProductCategory spc = new SampleProductCategory();
            foreach (ProductCategory pc in spc.SampleCategories)
            {
                db.ProductCategories.Add(pc);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddRestaurants()
        {

            SampleRestaurant sr = new SampleRestaurant();

            foreach (Restaurant r in sr.AllRestaurants)
            {
                db.Restaurants.Add(r);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            
        }

        public ActionResult AddProducts()
        {
            SampleProduct sp = new SampleProduct();
            var AllRestaurantsInDb = db.Restaurants.ToList();

            int productCount = 12;

            foreach (var r in AllRestaurantsInDb)
            {
                for (int i = 0; i < productCount; i++)
                {
                    //choose a random product
                    Product product = sp.GetRandomProduct();
                    //change its restaurantId to r
                    product.Restaurant = r;

                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");


            //var controller = DependencyResolver.Current.GetService<ProductController>();
            //controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

            //var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            //Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            //if (Rcontroller == null)
            //    System.Diagnostics.Debug.WriteLine("xd");
            //else
            //    System.Diagnostics.Debug.WriteLine("she'll be right");

            //List<Product> ExampleProducts = new List<Product>();
            //Product Product1 = new Product
            //{
            //    Name = "Whopper Value Meal",
            //    Restaurant = RestaurantController.GetRestaurant(db, "Hungry Jacks"),
            //    Category = new ProductCategory
            //    {
            //        Name = "Burgers"
            //    },
            //    Price = 11.65,
            //    Description = "Comes with 1 whopper burger, large fries and a large Coke. Yum Yum"
            //};
            //System.Diagnostics.Debug.WriteLine(Product1.Restaurant.Description + "here");
            //Product Product2 = new Product
            //{
            //    Name = "Oreo McFlurry",
            //    Restaurant = RestaurantController.GetRestaurant(db, "McDonalds"),
            //    Category = new ProductCategory
            //    {
            //        Name = "Ice Cream"
            //    },
            //    Price = 4.50,
            //    Description = "Very yum!!!!"
            //};

            //ExampleProducts.Add(Product1);
            //ExampleProducts.Add(Product2);

            //foreach(Product p in ExampleProducts)
            //{
            //    controller.Create(p);
            //}
            

            //return RedirectToAction("Index");
        }

        public ActionResult AddCustomers()
        {
            return RedirectToAction("Index");
        }

    }
}