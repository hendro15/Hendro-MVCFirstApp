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
        private SearchModel sm;
        // GET: Author
        public ActionResult AuthorProfile()
        {
            if (Session["LogedUserID"] != null)
            {
                db.readAuthor(int.Parse(Session["LogedUserID"].ToString()));
                var model = new AuthorAllModel();
                model.authorModel = new AuthorViewModel();
                model.authorModel.UserId = db.authorId;
                model.authorModel.Fullname = Session["LogedUserFullname"].ToString();
                model.authorModel.email = db.authorEmail;
                model.authorModel.Affiliasi = db.authorAf;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult MyAccount()
        {
            if (Session["LogedUserID"] != null)
            {
                db.readAuthor(int.Parse(Session["LogedUserID"].ToString()));
                var model = new AuthorAllModel();
                model.authorModel = new AuthorViewModel();
                model.authorModel.UserId = db.authorId;
                model.authorModel.Fullname = Session["LogedUserFullname"].ToString();
                model.authorModel.email = db.authorEmail;
                model.authorModel.Affiliasi = db.authorAf;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SearchResult(string key)
        {
            this.sm = new SearchModel();

            if(key != null)
            {
                sm.key = key;
                return View(sm);
            }
            else
            {
                return RedirectToAction("AuthorProfile", "Author");
            }
            
        }

        [HttpPost]
        public ActionResult Search(AuthorAllModel allModel)
        {
            string key = allModel.searchModel.key;
            return RedirectToAction("SearchResult", "Author", new { key = key});
        }
    }
}