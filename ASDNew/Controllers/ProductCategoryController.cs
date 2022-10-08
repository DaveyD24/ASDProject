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

        ASDContext3 db = new ASDContext3();

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(ProductCategory category)
        {
            if (!ContainsCategory(category))
            {
                db.ProductCategories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public bool ContainsCategory(ProductCategory Category)
        {
            List<ProductCategory> xd = db.ProductCategories.ToList();
            foreach (ProductCategory pc in xd)
            {
                if (pc.Name.Equals(Category.Name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}