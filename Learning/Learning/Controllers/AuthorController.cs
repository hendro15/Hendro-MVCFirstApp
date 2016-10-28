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
        private DbHandler db = new DbHandler();
        // GET: Author
        public ActionResult AuthorProfile(AuthorViewModel authorModel)
        {
            if (Session["LogedUserID"] != null)
            {
                db.readAuthor(Session["LogedUserFullname"].ToString());
                authorModel.UserId = db.authorId;
                authorModel.Fullname = Session["LogedUserFullname"].ToString();
                authorModel.email = db.authorEmail;
                authorModel.Affiliasi = db.authorAf;
                return View(authorModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}