using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learning.Models;

namespace Learning.Controllers
{
    public class HomeController : Controller
    {

        private int userID = 12345;
        private String fullname = "Michael Angelo";
        private String email = "user@email.com";
        private String pass = "12345";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel lm)
        {
            if (ModelState.IsValid)
            {
                if(lm.Email.Equals(email) && lm.Password.Equals(pass))
                {
                    Session["LogedUserID"] = userID;
                    Session["LogedUserFullname"] = fullname;
                    return RedirectToAction("AuthorProfile", "Author");
                }
               // System.Diagnostics.Debug.WriteLine("This is email : " + lm.Email);
            }
            return View(lm);
        }

    }

    
}