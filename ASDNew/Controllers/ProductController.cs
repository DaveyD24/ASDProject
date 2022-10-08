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

        /// <summary>
        /// View the Restaurant menu
        /// </summary>
        /// <param name="RestaurantID">Restaurant to filter products by</param>
        /// <returns>Product Index for given Restaurant</returns>
        public ActionResult Index(int? RestaurantID)
        {
            if (RestaurantID == null)
            {
                return View("~/Views/Error/Index.cshtml");
            }
            
            //Get current instance of Restaurant Controller
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            //Pass this Restaurant into View
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantID);
            ViewData["Restaurant"] = Restaurant;

            //Get all products sold by this restaurant
            List<Product> AllProducts = GetAllProducts(db);
            List<Product> FilteredProducts = FilterProductList(Restaurant, AllProducts);
            
            //Get all categories that this restaurant sells products in
            List<ProductCategory> RelevantCategories = GetRelevantCategories(FilteredProducts);
            ViewData["Categories"] = RelevantCategories;

            return View(FilteredProducts);
        }

        /// <summary>
        /// Get all products in the Products table
        /// </summary>
        /// <param name="db">Database instance</param>
        /// <returns>All products in Products table</returns>
        public static List<Product> GetAllProducts(ASDContext3 db)
        {
            List<Product> AllProducts = new List<Product>();
            AllProducts = db.Products
                .Include(a => a.Category)
                .Include(a => a.Restaurant)
                .ToList();
            return AllProducts;
        }

        /// <summary>
        /// Filters a list of products by restaurant
        /// </summary>
        /// <param name="Restaurant">Restaurant to filter by</param>
        /// <param name="Products">Initial Product list before filtering</param>
        /// <returns>Filter product list</returns>
        public static List<Product> FilterProductList(Restaurant Restaurant, List<Product> Products)
        {
            List<Product> FilteredProducts = new List<Product>();
            foreach (Product Product in Products)
            {
                if (Product.Restaurant.Id == Restaurant.Id)
                {
                    FilteredProducts.Add(Product);
                }
            }
            return FilteredProducts;
        }

        /// <summary>
        /// Get all ProductCategories contained in a list of products
        /// </summary>
        /// <param name="Products">List of Products</param>
        /// <returns>All ProductCategories contained in the list</returns>
        public static List<ProductCategory> GetRelevantCategories(List<Product> Products)
        {
            List<ProductCategory> RelevantCategories = new List<ProductCategory>();
            foreach (Product Product in Products)
            {
                if (!RelevantCategories.Contains(Product.Category))
                {
                    RelevantCategories.Add(Product.Category);
                }
            }
            return RelevantCategories;
        }

        /// <summary>
        /// Gets the first instance of this ProductCategory in the database
        /// </summary>
        /// <param name="db">Database instance</param>
        /// <param name="ProductCategoryName">Name of ProductCategory</param>
        /// <returns>ProductCategory object</returns>
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

        /// <summary>
        /// Gets the first instance of this ProductCategory in the database
        /// </summary>
        /// <param name="db">Database instance</param>
        /// <param name="ProductCategoryID">Id of ProductCategory</param>
        /// <returns>ProductCategory object</returns>
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

        /// <summary>
        /// Get all ProductCategories in the database
        /// </summary>
        /// <param name="db">Database Instance</param>
        /// <returns>List of ProductCategories</returns>
        public static List<ProductCategory> GetProductCategories(ASDContext3 db)
        {
            return db.ProductCategories.ToList();
        }

        /// <summary>
        /// Adds product to database
        /// </summary>
        /// <param name="product">Product object to add</param>
        /// <returns>Redirect to Product/Index</returns>
        public ActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Load AddProduct page
        /// </summary>
        /// <param name="RestaurantId">Restaurant to add product to</param>
        /// <returns>AddProduct Page</returns>
        public ActionResult AddProduct(int RestaurantId)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, RestaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(AllCategories);
        }

        /// <summary>
        /// Edits existing product entry in the database
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        /// <param name="RestaurantId">Id of Restaurant</param>
        /// <returns>EditProduct Page</returns>
        public ActionResult EditProduct(int ProductId, int RestaurantId)
        {
            Product Product = db.Products.Find(ProductId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(Product);
        }

        /// <summary>
        /// Deletes existing product entry from the database
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        /// <param name="RestaurantId">Id of Restaurant</param>
        /// <returns>DeleteProduct Page</returns>
        public ActionResult DeleteProduct(int ProductId, int RestaurantId)
        {
            Product Product = db.Products.Find(ProductId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;
            return View(Product);
        }

        /// <summary>
        /// Posts information from CreateProduct form
        /// </summary>
        /// <param name="RestaurantId">Id of Restaurant entered on form</param>
        /// <param name="ProductCategory">Id of ProductCategory entered on form</param>
        /// <param name="ProductName">Name of Product entered on form</param>
        /// <param name="ProductPrice">Price of Product entered on form</param>
        /// <param name="ProductDescription">Description of Product entered on form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int RestaurantId, int ProductCategory, string ProductName, double ProductPrice, string ProductDescription)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            ProductCategory NewCategory = null;
            foreach(ProductCategory Category in AllCategories)
            {
                if (Category.Id.Equals(ProductCategory))
                {
                    NewCategory = Category;
                }
            }

            Product Product = new Product
            {
                Restaurant = RestaurantController.GetRestaurantForDBOperation(db, RestaurantId),
                Category = NewCategory,
                Name = ProductName,
                Price = ProductPrice,
                Description = ProductDescription
            };

            db.Products.Add(Product);
            db.SaveChanges();

            return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
        }

        /// <summary>
        /// Posts form data to edit an existing Product in the database
        /// </summary>
        /// <param name="ProductID">Id of product entered on form</param>
        /// <param name="RestaurantId">Id of restaurant entered on form</param>
        /// <param name="ProductCategory">Id of ProductCategory entered on form</param>
        /// <param name="ProductName">Name of product entered on form</param>
        /// <param name="ProductPrice">Price of product entered on form</param>
        /// <param name="ProductDescription">Description of product entered on form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int ProductId, int RestaurantId, int ProductCategory, string ProductName, double ProductPrice, string ProductDescription)
        {
            var Rcontroller = DependencyResolver.Current.GetService<RestaurantController>();
            Rcontroller.ControllerContext = new ControllerContext(this.Request.RequestContext, Rcontroller);

            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            ProductCategory NewProductCategory = null;
            foreach (ProductCategory Category in AllCategories)
            {
                if (Category.Id.Equals(ProductCategory))
                {
                    NewProductCategory = Category;
                }
            }

            Product Product = new Product
            {
                Restaurant = RestaurantController.GetRestaurantForDBOperation(db, RestaurantId),
                Category = NewProductCategory,
                Name = ProductName,
                Price = ProductPrice,
                Description = ProductDescription
            };

            Product Entity = db.Products.FirstOrDefault(item => item.Id == ProductId);

            if (Entity != null)
            {
                Entity.Category = NewProductCategory;
                Entity.Name = ProductName;
                Entity.Price = ProductPrice;
                Entity.Description = ProductDescription;

                // Save changes to database
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
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
                System.Diagnostics.Debug.WriteLine("EditProduct entity is null");
                return View("Error");
            }
        }
    }
}