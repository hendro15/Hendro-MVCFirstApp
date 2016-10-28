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

        //private int userID = 12345;
        //private String fullname = "Michael Angelo";
        //private String email = "user@email.com";
        //private String pass = "12345";

        private int adId = 999;
        private String adName = "admin";
        private String adEmail = "admin@email.com";
        private String adPass = "admin";

        private DbHandler db = new DbHandler();

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
            db.loginAuthor(lm.Email, lm.Password);

            if (ModelState.IsValid)
            {
                if (lm.Email.Equals(db.authorEmail) && lm.Password.Equals(db.authorPass))
                {
                    Session["LogedUserID"] = db.authorId;
                    Session["LogedUserFullname"] = db.authorName;
                    return RedirectToAction("AuthorProfile", "Author");
                }
                else if (lm.Email.Equals(adEmail) && lm.Password.Equals(adPass))
                {
                    Session["LogedUserID"] = adId;
                    Session["LogedUserFullname"] = adName;

                    return RedirectToAction("Article", "Admin");
                }
                // System.Diagnostics.Debug.WriteLine("This is email : " + lm.Email);
            }
            return View(lm);
        }

    }


}