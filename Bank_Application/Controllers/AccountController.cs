using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bank_Application.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Web.Security;
using System.Collections.ObjectModel;

namespace Bank_Application.Controllers
{
    public class AccountController : Controller
    {
        BankApplicationEntities14 entity = new BankApplicationEntities14();


        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel pass)
        {
            var changePwd = entity.UsersTb1.FirstOrDefault(x => x.Email == pass.Email);
            if (changePwd != null)
            {
                changePwd.Passcode = pass.NewPassword;
            }
            entity.UsersTb1.Attach(changePwd);
            entity.Entry(changePwd).State = EntityState.Modified;
            entity.SaveChanges();

            return RedirectToAction("Login", "Account");

            TempData["SuccessMessage"] = "Transfer successful.";

        }

        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExists = entity.UsersTb1.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            UsersTb1 u = entity.UsersTb1.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode == credentials.Password);

            if (userExists)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UsersTb1 userinfo)
        {
            if (entity.UsersTb1.Any(x => x.Username == userinfo.Username))
            {
                ViewBag.Notification = "This Account has already existed";
                return View();
            }
            else
            {
                entity.UsersTb1.Add(userinfo);
                entity.SaveChanges();
                Session["Username"] = userinfo.Username.ToString();
                Session["Email"] = userinfo.Email.ToString();
                TempData["AlertMessage"] = "Congratulations, Registered Successfully...!";
                return RedirectToAction("Login");
                return View();
                //return RedirectToAction("Index", "Home"); 
            }

            /*entity.UsersTb1.Add(userinfo);
            entity.SaveChanges();
            TempData["AlertMessage"] = "Congratulations, Registered Successfully...!";
            return RedirectToAction("Login");
            return View();*/

        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        // [AllowAnonymous, HttpGet("ResetPassword")]
      

       

    }
}




    
