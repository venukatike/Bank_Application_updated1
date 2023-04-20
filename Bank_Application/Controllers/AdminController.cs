using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bank_Application.Models;
using System.Linq;

namespace Bank_Application.Controllers
{
   
    public class AdminController : Controller
    {

        BankApplicationEntities14 entity = new BankApplicationEntities14();
        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel credentials)
        {
            bool EmailExists = entity.Admins.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            Admin u = entity.Admins.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode == credentials.Password);

            if (EmailExists)
            {
                FormsAuthentication.SetAuthCookie(u.Email, false);
                return RedirectToAction("AdminPage");
            }

            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPage(Admin ad)
        {
            return View();
        }

    }
}


