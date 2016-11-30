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
        private AuthorAllModel allModel;
        private Author model;

        public ActionResult MyProfile()
        {
            if (Session["LogedUserID"] != null)
            {
                model = new Author();
                this.allModel = new AuthorAllModel();

                allModel.authorModel = model.researcher(int.Parse(Session["LogedUserID"].ToString()));
                return RedirectToAction("AuthorProfile", "Author", new { id = int.Parse(Session["LogedUserID"].ToString()) });

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult AuthorProfile(int id)
        {
            if (id != 0)
            {
                model = new Author();
                this.allModel = new AuthorAllModel();

                allModel.authorModel = model.researcher(id);
                return View(allModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult SearchResult(string key)
        {
            this.allModel = new AuthorAllModel();
            allModel.searchAuthor = new SearchAuthor();
            model = new Author();

            if (key != null)
            {
                Session["Keywords"] = key;
                allModel.searchAuthor.authorList = model.researcherList(key);
                allModel.searchAuthor.key = key;

                return View(allModel);
            }
            else
            {
                return RedirectToAction("AuthorProfile", "Author", new { id = int.Parse(Session["LogedUserID"].ToString()) });
            }

        }

        [HttpPost]
        public ActionResult Search(AuthorAllModel allModel)
        {
            string key = allModel.searchAuthor.key;
            return RedirectToAction("SearchResult", "Author", new { key = key });
        }

        public ActionResult MergeAction(int id)
        {
            model = new Author();

            if (Session["LogedUserID"] != null)
            {
                string key = Session["Keywords"].ToString();
                model.merge(int.Parse(Session["LogedUserID"].ToString()), id);
                return RedirectToAction("SearchResult", "Author", new { key = key });
            }
            else
            {
                return RedirectToAction("SearchResult", "Author");
            }

        }
    }
}