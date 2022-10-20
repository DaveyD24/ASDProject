using ASDNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASDNew.Controllers
{
    public class ProductCategoryController : Controller
    {

        //Database instance
        ASDContext9 db = new ASDContext9();

        /// <summary>
        /// Default Index
        /// </summary>
        /// <returns>ProductCategory/Index</returns>
        public ActionResult Index()
        {
            // Retrieve product count grouped by category
            var productCounts = from Product in db.Products
                                group Product by Product.Category.Id into g
                                select new
                                {
                                    Category = g.Key,
                                    TotalProducts = g.Count()
                                };

            ViewData["ProductCounts"] = productCounts.ToDictionary(t => t.Category, t => t.TotalProducts);

            return View(db.ProductCategories);
        }

        public ActionResult AddProductCategory()
        {
            var productCategories = db.ProductCategories.ToList();
            return View(productCategories);
        }

        public ActionResult EditProductCategory(int? CategoryId)
        {
            if (CategoryId == null)
            {
                System.Diagnostics.Debug.WriteLine("One or more parameters is null");
                return View("Error");
            }

            ProductCategory productCategory = db.ProductCategories.Find(CategoryId);
            if (productCategory == null)
            {
                System.Diagnostics.Debug.WriteLine("The requested product category could not be found.");
                TempData["ErrorMessage"] = "The requested product category could not be found.";
                return RedirectToAction("Index", "ProductCategory");
            }

            return View(productCategory);
        }

        public ActionResult DeleteProductCategory(int? CategoryId)
        {
            if (CategoryId == null)
            {
                System.Diagnostics.Debug.WriteLine("One or more parameters is null");
                return View("Error");
            }

            ProductCategory productCategory = db.ProductCategories.Find(CategoryId);
            if (productCategory == null)
            {
                System.Diagnostics.Debug.WriteLine("The requested product category could not be found.");
                TempData["ErrorMessage"] = "The requested product category could not be found.";
                return RedirectToAction("Index", "ProductCategory");
            }

            return View(productCategory);
        }

        /// <summary>
        /// Create a new ProductCategory in the database
        /// </summary>
        /// <param name="Category">Category to add</param>
        /// <returns>Redirect to ProductCategory/Index</returns>
        [HttpPost]
        public ActionResult Create(string CategoryName)
        {
            // Remove leading and trailing spaces from name
            CategoryName = CategoryName.Trim();

            if (CategoryName.Length <= 0)
            {
                // Display error message if the category name is blank
                TempData["ErrorMessage"] = "Please enter a product category name.";
                return RedirectToAction("AddProductCategory", "ProductCategory");
            }

            ProductCategory productCategory = new ProductCategory
            {
                Name = CategoryName
            };

            if (ContainsCategory(productCategory))
            {
                // Display error message if the category name already exists
                TempData["ErrorMessage"] = "Product category name already exists. Please enter a different name.";
                return RedirectToAction("AddProductCategory", "ProductCategory");
            }

            // Add to database
            try
            {
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product category added successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.StackTrace);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                return RedirectToAction("AddProductCategory", "ProductCategory");
            }
        }

        /// <summary>
        /// Edit an existing ProductCategory in the database
        /// </summary>
        /// <param name="Category">Category to edit</param>
        /// <returns>Redirect to ProductCategory/Index</returns>
        [HttpPost]
        public ActionResult Edit(int CategoryId, string CategoryName)
        {
            // Remove leading and trailing spaces from name
            CategoryName = CategoryName.Trim();

            // Retrieve existing category from database
            ProductCategory ExistingCategory = db.ProductCategories.FirstOrDefault(item => item.Id == CategoryId);

            // Check category ID exists
            if (ExistingCategory == null)
            {
                System.Diagnostics.Debug.WriteLine("Category ID could not be found.");
                return View("Error");
            }

            if (CategoryName.Length <= 0)
            {
                // Display error message if the category name is blank
                TempData["ErrorMessage"] = "Please enter a product category name.";
                return RedirectToAction("EditProductCategory", "ProductCategory", new { CategoryId });
            }

            // Create a new temporary Product Category instance to test for duplicate names
            ProductCategory productCategoryClone = new ProductCategory
            {
                Name = CategoryName
            };

            if (productCategoryClone.Name == ExistingCategory.Name)
            {
                // Display error message if new name matches current name
                TempData["ErrorMessage"] = "New product category name is identical to current name.";
                return RedirectToAction("EditProductCategory", "ProductCategory", new { CategoryId });
            }

            if (ContainsCategory(productCategoryClone))
            {
                // Display error message if the category name already exists
                TempData["ErrorMessage"] = "Product category name already exists. Please enter a different name.";
                return RedirectToAction("EditProductCategory", "ProductCategory", new { CategoryId });
            }

            // Make change and save to database
            try
            {
                ExistingCategory.Name = CategoryName;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product category updated successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.StackTrace);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                return RedirectToAction("EditProductCategory", "ProductCategory", new { CategoryId });
            }
        }

        /// <summary>
        /// Delete an existing ProductCategory in the database
        /// </summary>
        /// <param name="Category">Category to delete</param>
        /// <returns>Redirect to ProductCategory/Index</returns>
        [HttpPost]
        public ActionResult Delete(int CategoryId)
        {
            // Retrieve existing category from database
            ProductCategory ExistingCategory = db.ProductCategories.FirstOrDefault(item => item.Id == CategoryId);

            // Check category ID exists
            if (ExistingCategory == null)
            {
                System.Diagnostics.Debug.WriteLine("Category ID could not be found.");
                return View("Error");
            }

            // Delete and save to database
            try
            {
                db.ProductCategories.Remove(ExistingCategory);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Product category deleted successfully!";
                return RedirectToAction("Index", "ProductCategory");
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(E.StackTrace);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                return RedirectToAction("DeleteProductCategory", "ProductCategory", new { CategoryId });
            }
        }

        /// <summary>
        /// Checks if Category already exists in the database
        /// </summary>
        /// <param name="Category">Category to check</param>
        /// <returns>Whether database already contains this category</returns>
        public bool ContainsCategory(ProductCategory Category)
        {
            List<ProductCategory> AllCategories = db.ProductCategories.ToList();
            foreach (ProductCategory ProductCategory in AllCategories)
            {
                if (ProductCategory.Name.ToLower().Equals(Category.Name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}