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
            return View();
        }

        /// <summary>
        /// Create a new ProductCategory in the database
        /// </summary>
        /// <param name="Category">Category to add</param>
        /// <returns>Redirect to ProductCategory/Index</returns>
        public ActionResult Create(ProductCategory Category)
        {
            if (!ContainsCategory(Category))
            {
                db.ProductCategories.Add(Category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
                if (ProductCategory.Name.Equals(Category.Name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}