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
        public static int TestCount = 4;

        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                System.Diagnostics.Debug.WriteLine("xdddddddddd");
                //Display Error Page
            }


            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantID);
            ViewData["Restaurant"] = Restaurant;

            List<Product> AllProducts = GetAllProducts(db);
            List<Product> FilteredProducts = FilterProductList(Restaurant, AllProducts);
            List<ProductCategory> RelevantCategories = GetRelevantCategories(FilteredProducts);

            ViewData["Categories"] = RelevantCategories;

            return View(FilteredProducts);
        }

        public static List<Product> GetAllProducts(ASDContext3 db)
        {
            List<Product> AllProducts = new List<Product>();
            AllProducts = db.Products
                .Include(a => a.Category)
                .Include(a => a.Restaurant)
                .ToList();
            return AllProducts;
        }

        public static List<Product> FilterProductList(Restaurant Restaurant, List<Product> Products)
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

        public static List<ProductCategory> GetRelevantCategories(List<Product> Products)
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

        public static ProductCategory GetCategory(ASDContext3 db, string ProductCategoryName)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            foreach (ProductCategory Category in AllCategories)
            {
                if (Category.Name.Equals(ProductCategoryName))
                {
                    return Category;
                }
            }
            return null;
        }

        public static ProductCategory GetCategory(ASDContext3 db, int? ProductCategoryID)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            foreach (ProductCategory Category in AllCategories)
            {
                if (Category.Id == (int)ProductCategoryID)
                {
                    return Category;
                }
            }
            return null;
        }

        public static List<ProductCategory> GetProductCategories(ASDContext3 db)
        {
            return db.ProductCategories.ToList();
        }

        //public void AddToCart(string test)
        //{
        //    System.Diagnostics.Debug.WriteLine(test);
        //}

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

        public ActionResult AddProduct(int restaurantId)
        {
            List<ProductCategory> categories = db.ProductCategories.ToList();
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, restaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(categories);
        }

        public ActionResult EditProduct(int prodId, int restaurantId)
        {
            Product product = db.Products.Find(prodId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)restaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(product);
        }

        public ActionResult DeleteProduct(int prodId, int restaurantId)
        {
            Product product = db.Products.Find(prodId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)restaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(int restaurantId, int prodCategory, string prodName, double prodPrice, string prodDesc)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);


            List<ProductCategory> Cats = db.ProductCategories.ToList();
            ProductCategory Cat = null;
            foreach (var c in Cats)
            {
                if (c.Id.Equals(prodCategory))
                {
                    Cat = c;
                }
            }

            Product product = new Product
            {
                Restaurant = RestaurantController.GetRestaurantForDBOperation(db, restaurantId),
                Category = Cat,
                Name = prodName,
                Price = prodPrice,
                Description = prodDesc
            };
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index", "Product", new { RestaurantID = restaurantId });
        }

        [HttpPost]
        public ActionResult Edit(int prodId, int restaurantId, int prodCategory, string prodName, double prodPrice, string prodDesc)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            List<ProductCategory> Cats = db.ProductCategories.ToList();
            ProductCategory Cat = null;
            foreach (var c in Cats)
            {
                if (c.Id.Equals(prodCategory))
                {
                    Cat = c;
                }
            }

            Product product = new Product
            {
                Restaurant = RestaurantController.GetRestaurantForDBOperation(db, restaurantId),
                Category = Cat,
                Name = prodName,
                Price = prodPrice,
                Description = prodDesc
            };

            var entity = db.Products.FirstOrDefault(item => item.Id == prodId);

            if (entity != null)
            {
                entity.Category = Cat;
                entity.Name = prodName;
                entity.Price = prodPrice;
                entity.Description = prodDesc;

                // Save changes to database
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product", new { RestaurantID = restaurantId });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    return View("Error");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("EditProduct entity is null");
                return View("Error");
            }


        }

        public ActionResult Delete(int prodId, int restaurantId, int prodCategory, string prodName, double prodPrice, string prodDesc)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            // TODO: Complete implementation, Set deleted flag = true

            return View("Error");
        }

        // CART STUFF
        public ActionResult AddToCart(int prodId, int restaurantId)
        {
            double totalprice;
            if (Session["cart"] == null)
            {
                List<Product> cart = new List<Product>();
                var product = db.Products.Find(prodId);
                cart.Add(new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                });
                Session["cart"] = cart;
                totalprice = 0;
                totalprice += db.Products.Find(prodId).Price;
                Session["totalamount"] = totalprice;
            }
            else
            {
                List<Product> cart = (List<Product>)Session["cart"];
                var product = db.Products.Find(prodId);
                cart.Add(new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                });
                Session["cart"] = cart;
                double temp = Double.Parse(Session["totalamount"].ToString());
                totalprice = db.Products.Find(prodId).Price;
                Session["totalamount"] = totalprice + temp;
            }


            return RedirectToAction("Index", "Product", new { RestaurantID = restaurantId });
        }

        public ActionResult RemoveAllFromCart(int restaurantId)
        {
            if (Session["cart"] != null)
            {
                List<Product> cart = new List<Product>();
                Session["cart"] = cart;
                Session["totalamount"] = 0;
            }
            return RedirectToAction("Index", "Product", new { RestaurantID = restaurantId });
        }

        public ActionResult RemoveFromCart(int prodId, int restaurantId)
        {
            if (Session["cart"] != null)
            {
                List<Product> cart = (List<Product>)Session["cart"];
                Session["cart"] = cart.Where(m => m.Id != prodId).ToList();
                double totalprice = db.Products.Find(prodId).Price;
                Session["totalamount"] = Double.Parse(Session["totalamount"].ToString()) - totalprice;
                if (Double.Parse(Session["totalamount"].ToString()) <= 0.5)
                {
                    Session["totalamount"] = 0;
                }
            }

            return RedirectToAction("Index", "Product", new { RestaurantID = restaurantId });
        }

        public ActionResult ContinuetoCheckout ()
        {
            

            return RedirectToAction("PaymentPage", "Payment");
        }
    }
}