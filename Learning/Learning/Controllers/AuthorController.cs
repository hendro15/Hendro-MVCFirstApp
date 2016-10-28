using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Learning.Models;

namespace Learning.Controllers
{
    public class AuthorController : Controller
    {
        
        // GET: Author
        public ActionResult AuthorProfile(AuthorViewModel authorModel)
        {
            if(authorModel.Fullname != null)
            {
                return View(authorModel);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}