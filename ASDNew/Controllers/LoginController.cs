using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using ASDNew.Models;

namespace ASDNew.Controllers
{
    public class LoginController : Controller
    {
        //private DB_Entities _db = new DB_Entities();
        ASDContext8 _db = new ASDContext8();
        // GET: Home
        public ActionResult Index()
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

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer _customer)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Customers.FirstOrDefault(s => s.Email == _customer.Email);
                if (check == null)
                {
                    _customer.Password = GetMD5(_customer.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Customers.Add(_customer);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLogin _customerlogin)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(_customerlogin.Password);
                var data = _db.Customers.Where(s => s.Email.Equals(_customerlogin.Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["Id"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Customers.FirstOrDefault(s => s.Email == customer.Email);
                if (check == null)
                {
                    int id = Convert.ToInt32(Session["Id"].ToString());
                    Customer customers = _db.Customers.FirstOrDefault(x => x.Id == id);
                    if (customers != null)
                    {
                        customers.FirstName = customer.FirstName;
                        customers.LastName = customer.LastName;
                        customers.Email = customer.Email;
                        customers.Password = customer.Password;
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View("EditUserDetails");
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            System.Diagnostics.Debug.WriteLine("errors");
            return View("Login");
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
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