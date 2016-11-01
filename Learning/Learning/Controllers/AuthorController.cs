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
        private AuthorAllModel allModel;
        // GET: Author
        public ActionResult AuthorProfile()
        {
            if (Session["LogedUserID"] != null)
            {

                db.readAuthor(int.Parse(Session["LogedUserID"].ToString()));
                this.allModel = new AuthorAllModel();
                allModel.authorModel = new AuthorViewModel();
                allModel.authorModel.UserId = db.authorId;
                allModel.authorModel.Fullname = db.authorName;
                allModel.authorModel.email = db.authorEmail;
                allModel.authorModel.Affiliasi = db.authorAf;
                allModel.authorModel.penulis = db.penulis;
                return View(allModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        //public ActionResult MyAccount()
        //{
        //    if (Session["LogedUserID"] != null)
        //    {
        //        db.readAuthor(int.Parse(Session["LogedUserID"].ToString()));
        //        var model = new AuthorAllModel();
        //        model.authorModel = new AuthorViewModel();
        //        model.authorModel.UserId = db.authorId;
        //        model.authorModel.Fullname = Session["LogedUserFullname"].ToString();
        //        model.authorModel.email = db.authorEmail;
        //        model.authorModel.Affiliasi = db.authorAf;
        //        return View(model.authorModel);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SearchResult(string key)
        {
            this.allModel = new AuthorAllModel();
            allModel.searchModel = new SearchModel();
            if (key != null)
            {
                Session["Keywords"] = key;
                db.searchProfile(key);

                allModel.searchModel.searchResult = db.searchResult;

                return View(allModel);
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
            return RedirectToAction("SearchResult", "Author", new { key = key });
        }

        public ActionResult MergeAction(int id)
        {
            if (Session["LogedUserID"] != null)
            {
                string key = Session["Keywords"].ToString();
                db.mergeAccount(int.Parse(Session["LogedUserID"].ToString()), id);
                return RedirectToAction("SearchResult", "Author", new { key = key });
            }
            else
            {
                return RedirectToAction("SearchResult", "Author");
            }

        }
    }
}