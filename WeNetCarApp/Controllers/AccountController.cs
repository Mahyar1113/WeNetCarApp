using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WeNetCarApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (IsValidUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("Login");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
   
}