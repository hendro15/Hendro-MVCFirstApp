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

        private int adId = 999;
        private String adName = "admin";
        private String adEmail = "admin@email.com";
        private String adPass = "admin";

        private AuthorModel model;
        private AuthorAllModel allModel;
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
            model = new AuthorModel();
            allModel = new AuthorAllModel();

            allModel.authorModel = model.login(lm.Email, lm.Password);

            if (ModelState.IsValid)
            {
                if (lm.Email.Equals(allModel.authorModel.email) && lm.Password.Equals(allModel.authorModel.password))
                {
                    Session["LogedUserID"] = allModel.authorModel.userId;
                    Session["LogedUserFullname"] = allModel.authorModel.fullname;
                    return RedirectToAction("MyProfile", "Author");
                }
                else if (lm.Email.Equals(adEmail) && lm.Password.Equals(adPass))
                {
                    Session["LogedUserID"] = adId;
                    Session["LogedUserFullname"] = adName;

                    return RedirectToAction("Article", "Admin");
                }
            }
            return View(lm);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session["LogedUserID"] = null;
            Session["LogedUserFullname"] = null;

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Search(AuthorAllModel allModel)
        {
            if (allModel.searchModel.key != null)
            {
                string key = allModel.searchModel.key;
                return RedirectToAction("GuestSearchResult", "Home", new { key = key });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult GuestSearchResult(string key)
        {
            model = new AuthorModel();
            this.allModel = new AuthorAllModel();
            allModel.searchModel = new SearchModel();

            if (key != null)
            {
                allModel.searchModel.searchResult = model.researcherList(key);
                allModel.searchModel.key = key;
                return View(allModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            model = new AuthorModel();
            allModel = new AuthorAllModel();

            if (id > -1)
            {
                allModel.authorModel = model.researcher(id);
                return View(allModel);

            }
            else
            {
                string key = Session["Keywords"].ToString();
                return RedirectToAction("GuestSearchResult", "Home", new { key = key });
            }
        }
    }
}