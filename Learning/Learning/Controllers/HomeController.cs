﻿using System;
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
        private AuthorAllModel allModel;

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
                    return RedirectToAction("AuthorProfile", "Author", new { id = db.authorId });
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
            this.allModel = new AuthorAllModel();
            allModel.searchModel = new SearchModel();
            if (key != null)
            {
                Session["Keywords"] = key;
                db.searchAuthor(key);
                allModel.searchModel.searchResult = db.searchResult;
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
            this.allModel = new AuthorAllModel();
            allModel.authorModel = new AuthorViewModel();
            if (id > -1)
            {
                db.readAuthor(id);
                allModel.authorModel.UserId = db.authorId;
                allModel.authorModel.Fullname = db.authorName;
                allModel.authorModel.email = db.authorEmail;
                allModel.authorModel.Affiliasi = db.authorAf;
                allModel.authorModel.penulis = db.penulis;
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