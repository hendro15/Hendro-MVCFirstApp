using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learning.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult AuthorProfile()
        {
            if(Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
    }
}