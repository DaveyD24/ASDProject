using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;
//using static ASDNew.Models.Restaurant;

namespace ASDNew.Controllers
{

    public class ProductController : Controller
    {
        private ASDContext9 db = new ASDContext9();

        private readonly string productImageBasePath = "~/Images/Products/";
        private readonly string[] allowedImageFileExtensions = {".jpg", ".jpeg", ".png", ".gif"};

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
        public static List<Product> GetAllProducts(ASDContext9 db)
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
        public static ProductCategory GetCategory(ASDContext9 db, string ProductCategoryName)
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
        public static ProductCategory GetCategory(ASDContext9 db, int? ProductCategoryID)
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
        public static List<ProductCategory> GetProductCategories(ASDContext9 db)
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
        public ActionResult AddProduct(int? RestaurantId)
        {
            // Show error page if parameter is null
            if (RestaurantId == null)
            {
                System.Diagnostics.Debug.WriteLine("restaurantId parameter is null");
                return View("Error");
            }

            // Store data to be used by the View
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;

            // Show error page if restaurant ID does not exist
            if (Restaurant == null)
            {
                System.Diagnostics.Debug.WriteLine("Restaurant is null or could not be found");
                return View("Error");
            }

            return View(AllCategories);
        }

        /// <summary>
        /// Edits existing product entry in the database
        /// </summary>
        /// <param name="RestaurantId">Id of Product</param>
        /// <returns>EditProduct Page</returns>
        public ActionResult EditProduct(int? ProductId, int? RestaurantId)
        {
            // Show error page if parameters are null
            if (ProductId == null || RestaurantId == null)
            {
                System.Diagnostics.Debug.WriteLine("One or more parameters is null");
                return View("Error");
            }

            // Store data to be used by the View
            Product Product = db.Products.Find(ProductId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;

            // Show error page if restaurant ID does not exist
            if (Restaurant == null || Product == null)
            {
                System.Diagnostics.Debug.WriteLine("Restaurant and/or product is null or could not be found");
                return View("Error");
            }

            return View(Product);
        }

        /// <summary>
        /// Deletes existing product entry from the database
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        /// <param name="RestaurantId">Id of Restaurant</param>
        /// <returns>DeleteProduct Page</returns>
        public ActionResult DeleteProduct(int? ProductId, int? RestaurantId)
        {
            // Show error page if parameters are null
            if (ProductId == null || RestaurantId == null)
            {
                System.Diagnostics.Debug.WriteLine("One or more parameters is null");
                return View("Error");
            }

            // Store data to be used by the View
            Product Product = db.Products.Find(ProductId);
            ViewData["AllCategories"] = GetProductCategories(db);
            Restaurant Restaurant = RestaurantController.GetRestaurant(db, (int)RestaurantId);
            ViewData["Restaurant"] = Restaurant;

            // Show error page if restaurant ID does not exist
            if (Restaurant == null || Product == null)
            {
                System.Diagnostics.Debug.WriteLine("Restaurant and/or product is null or could not be found");
                return View("Error");
            }

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
        public ActionResult Create(int RestaurantId, int ProductCategory, string ProductName, double ProductPrice, string ProductDescription, HttpPostedFileBase ProductImage)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            string redirectToPage = "AddProduct";

            // Get the matching product category
            ProductCategory NewCategory = null;
            foreach(ProductCategory Category in AllCategories)
            {
                if (Category.Id.Equals(ProductCategory))
                {
                    NewCategory = Category;
                }
            }

            // Upload image
            string fileName = "";
            if (ProductImage != null)
            {
                string fileExtension = Path.GetExtension(ProductImage.FileName).ToLower();

                // Check image file format is allowed
                if (!allowedImageFileExtensions.Contains(fileExtension))
                {
                    string errorMessage = "Only the following image formats are allowed: ";
                    foreach (string s in allowedImageFileExtensions)
                    {
                        errorMessage += s + " ";
                    }

                    // Display error message if file format is not allowed
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction(redirectToPage, "Product", new { RestaurantID = RestaurantId });
                }

                // Check that file is less than 500 KB
                if (ProductImage.ContentLength > 500000)
                {
                    // Display error message if file is too large
                    System.Diagnostics.Debug.WriteLine("Image uploaded is too large");
                    TempData["ErrorMessage"] = "Uploaded image must be less than 500 KB.";
                    return RedirectToAction(redirectToPage, "Product", new { RestaurantID = RestaurantId });
                }

                // Generate random file name
                Random r = new Random();
                int fileNumber = r.Next(1, 1000000000);

                fileName = fileNumber + "." + ProductImage.FileName.Split('.').LastOrDefault();
                string filePath = Path.Combine(Server.MapPath(productImageBasePath), fileName);

                // Check if file already exists
                if (System.IO.File.Exists(filePath))
                {
                    System.Diagnostics.Debug.WriteLine("File already exists in the image directory: " + filePath);
                    TempData["ErrorMessage"] = "An unexpected error occurred while trying to upload the image. Please try again.";
                    return RedirectToAction(redirectToPage, "Product", new { RestaurantID = RestaurantId });
                }

                try
                {
                    ProductImage.SaveAs(filePath);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Product image upload failed: " + e.StackTrace);
                    TempData["ErrorMessage"] = "An unexpected error occurred while trying to upload the image. Please try again.";
                    return RedirectToAction(redirectToPage, "Product", new { RestaurantID = RestaurantId });
                }
            }

            // Create the new product using provided parameters
            Product Product = new Product
            {
                Restaurant = RestaurantController.GetRestaurantForDBOperation(db, RestaurantId),
                Category = NewCategory,
                Name = ProductName,
                Price = ProductPrice,
                Description = ProductDescription,
                Image = fileName.Length > 0 ? fileName : null
            };

            // Save the new product to database
            try
            {
                db.Products.Add(Product);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Product added successfully!";
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.Message);
                System.Diagnostics.Debug.WriteLine(E.StackTrace);
                return View("Error");
            }

            // Redirect user to restaurant product page
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
        public ActionResult Edit(int ProductId, int RestaurantId, int ProductCategory, string ProductName, double ProductPrice, string ProductDescription, HttpPostedFileBase ProductImage)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            string redirectToPage = "EditProduct";

            // Get the matching product category
            ProductCategory NewProductCategory = null;
            foreach (ProductCategory Category in AllCategories)
            {
                if (Category.Id.Equals(ProductCategory))
                {
                    NewProductCategory = Category;
                }
            }

            // Retrieve existing product from database
            Product Entity = db.Products.FirstOrDefault(item => item.Id == ProductId);

            // Check product ID exists
            if (Entity == null)
            {
                System.Diagnostics.Debug.WriteLine("EditProduct entity is null");
                return View("Error");
            }

            // Upload image
            string fileName = "";
            if (ProductImage != null)
            {
                string fileExtension = Path.GetExtension(ProductImage.FileName).ToLower();

                // Check image file format is allowed
                if (!allowedImageFileExtensions.Contains(fileExtension))
                {
                    string errorMessage = "Only the following image formats are allowed: ";
                    foreach (string s in allowedImageFileExtensions)
                    {
                        errorMessage += s + " ";
                    }

                    // Display error message if file format is not allowed
                    TempData["ErrorMessage"] = errorMessage;
                    return RedirectToAction(redirectToPage, "Product", new { ProductID = ProductId, RestaurantID = RestaurantId });
                }

                // Check that file is less than 500 KB
                if (ProductImage.ContentLength > 500000)
                {
                    // Display error message if file is too large
                    System.Diagnostics.Debug.WriteLine("Image uploaded is too large");
                    TempData["ErrorMessage"] = "Uploaded image must be less than 500 KB.";
                    return RedirectToAction(redirectToPage, "Product", new { ProductID = ProductId, RestaurantID = RestaurantId });
                }

                // Generate random file name
                Random r = new Random();
                int fileNumber = r.Next(1, 1000000000);

                fileName = fileNumber + "." + ProductImage.FileName.Split('.').LastOrDefault();
                string filePath = Path.Combine(Server.MapPath(productImageBasePath), fileName);

                // Check if file already exists
                if (System.IO.File.Exists(filePath))
                {
                    System.Diagnostics.Debug.WriteLine("File already exists in the image directory: " + filePath);
                    TempData["ErrorMessage"] = "An unexpected error occurred while trying to upload the image. Please try again.";
                    return RedirectToAction(redirectToPage, "Product", new { ProductID = ProductId, RestaurantID = RestaurantId });
                }

                try
                {
                    ProductImage.SaveAs(filePath);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Product image upload failed: " + e.StackTrace);
                    TempData["ErrorMessage"] = "An unexpected error occurred while trying to upload the image. Please try again.";
                    return RedirectToAction(redirectToPage, "Product", new { ProductID = ProductId, RestaurantID = RestaurantId });
                }
            }

            // Update product with new details
            Entity.Category = NewProductCategory;
            Entity.Name = ProductName;
            Entity.Price = ProductPrice;
            Entity.Description = ProductDescription;
            Entity.Image = ProductImage != null ? fileName : Entity.Image;  // 

            try
            {
                // Save changes to database
                db.SaveChanges();
                TempData["SuccessMessage"] = "Product updated successfully!";

                // Redirect user to restaurant product page
                return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.Message);
                System.Diagnostics.Debug.WriteLine(E.StackTrace);
                return View("Error");
            }

        }

        /// <summary>
        /// Posts form data to remove an existing Product from the database
        /// </summary>
        /// <param name="ProductId">Id of product entered on form</param>
        /// <param name="RestaurantId">Id of restaurant entered on form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int ProductId, int RestaurantId)
        {
            // Fetch product ID from database
            Product Entity = db.Products.FirstOrDefault(item => item.Id == ProductId);

            // Check product ID exists
            if (Entity != null)
            {
                try
                {
                    // Remove product record from database
                    db.Products.Remove(Entity);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Product deleted successfully!";
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
                System.Diagnostics.Debug.WriteLine("DeleteProduct entity is null");
                return View("Error");
            }
        }

        /// <summary>
        /// Adds product to cart
        /// </summary>
        /// <param name="ProductId">Product to Add</param>
        /// <param name="RestaurantId">Restaurant product is sold at</param>
        /// <returns></returns>
        public ActionResult AddToCart(int ProductId, int RestaurantId)
        {
            double TotalPrice;
            if (Session["Cart"] == null)
            {
                List<Product> Cart = new List<Product>();
                var Product = db.Products.Find(ProductId);
                Cart.Add(new Product()
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Description = Product.Description,
                    Price = Product.Price,
                });
                Session["Cart"] = Cart;
                TotalPrice = 0;
                TotalPrice += db.Products.Find(ProductId).Price;
                Session["TotalAmount"] = TotalPrice;
            }
            else
            {
                List<Product> Cart = (List<Product>)Session["Cart"];
                var Product = db.Products.Find(ProductId);
                Cart.Add(new Product()
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Description = Product.Description,
                    Price = Product.Price,
                });
                Session["Cart"] = Cart;
                double Temp = Double.Parse(Session["TotalAmount"].ToString());
                TotalPrice = db.Products.Find(ProductId).Price;
                Session["TotalAmount"] = TotalPrice + Temp;
            }

            return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
        }

        /// <summary>
        /// Remove every Product from the Cart
        /// </summary>
        /// <param name="RestaurantId">Restaurant page to return to</param>
        /// <returns>Product/Index View for specific Restaurant</returns>
        public ActionResult RemoveAllFromCart(int RestaurantId)
        {
            if (Session["Cart"] != null)
            {
                List<Product> Cart = new List<Product>();
                Session["Cart"] = Cart;
                Session["TotalAmount"] = 0;
            }
            return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
        }

        /// <summary>
        /// Removes an item from the Cart
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="RestaurantId"></param>
        /// <returns>Product/Index View for specific Resraurant</returns>
        public ActionResult RemoveFromCart(int ProductId, int RestaurantId)
        {
            if (Session["Cart"] != null)
            {
                List<Product> Cart = (List<Product>)Session["Cart"];
                Session["Cart"] = Cart.Where(m => m.Id != ProductId).ToList();
                double TotalPrice = db.Products.Find(ProductId).Price;
                Session["TotalAmount"] = Double.Parse(Session["TotalAmount"].ToString()) - TotalPrice;
                if (Double.Parse(Session["TotalAmount"].ToString()) <= 0.5)
                {
                    Session["TotalAmount"] = 0;
                }
            }
            return RedirectToAction("Index", "Product", new { RestaurantID = RestaurantId });
        }

        /// <summary>
        /// Jumps to payment page
        /// </summary>
        /// <returns>Payment/PaymentPage View</returns>
        public ActionResult ContinuetoCheckout()
        {
            return RedirectToAction("PaymentPage", "Payment");
        }
    }
}