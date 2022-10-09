using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASDNew.Models;
//using static ASDNew.Models.Restaurant;

namespace ASDNew.Controllers
{

    public class CustomerController : Controller
    {
        private ASDContext3 db = new ASDContext3();

        // GET: Product
        public ActionResult Index()
        {
            var restaurants = from r in db.Customers
                              orderby r.Id
                              select r;
            return View(restaurants);
        }

        public ActionResult EditUserDetails()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Create(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (Session["Id"] != null)
                {
                    customer.Password = GetMD5(customer.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    int id = Convert.ToInt32(Session["Id"].ToString());
                    Customer customers = db.Customers.FirstOrDefault(x => x.Id == id);
                    if (customers != null)
                    {
                        customers.FirstName = customer.FirstName;
                        customers.LastName = customer.LastName;
                        customers.Email = customer.Email;
                        customers.Password = customer.Password;
                        try
                        {
                            db.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        catch (Exception E)
                        {
                            System.Diagnostics.Debug.WriteLine(E.Message);
                            System.Diagnostics.Debug.WriteLine(E.StackTrace);
                            return View("About", "Admin");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("1");
                        return View("Contact", "Admin");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("2");
                    return View("Index");
                }
            }
            else
            {
                ViewBag.error = "Email already exists";
                System.Diagnostics.Debug.WriteLine("2");
                return View("Error");
            }
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }

}

//public ActionResult Delete()
//{

//}

