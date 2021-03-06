﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learning.Models;
using System.Data;

namespace Learning.Controllers
{
    public class HomeController : Controller
    {

        private int adId = 999;
        private String adName = "admin";
        private String adEmail = "admin@email.com";
        private String adPass = "admin";

        private Author model;
        private AuthorAllModel allModel;
        private AuthorController authorControl;
        private Account account;

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
            model = new Author();
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel rm)
        {
            account = new Account();

            if(rm.Email != null && rm.Password != null && rm.Fullname != null)
            {
                if(rm.Password == rm.ConfirmPassword)
                {
                    account.Register(rm.Email, rm.Password, rm.Fullname);
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return RedirectToAction("Register", "Home");
                }
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
        }

        [HttpPost]
        public ActionResult Search(AuthorAllModel allModel)
        {
            if (allModel.searchAuthor.key != null)
            {
                Session["keywords"] = allModel.searchAuthor.key;
                return RedirectToAction("SearchResult", "Home", new { key = allModel.searchAuthor.key });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult SearchResult(string key)
        {
            model = new Author();
            this.allModel = new AuthorAllModel();
            allModel.searchAuthor = new SearchAuthor();

            if (key != null)
            {
                allModel.searchAuthor.authorList = model.authorList(key);
                allModel.searchAuthor.key = key;
                return View(allModel);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult AuthorProfile(int id)
        {
            authorControl = new AuthorController();

            if (id > 0)
            {
                return RedirectToAction("AuthorProfile", "Author", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult MergeAction(int id)
        {
            authorControl = new AuthorController();

            if(id > 0)
            {
                return RedirectToAction("MergeAction", "Author", new { id = id });
            }
            else
            {
                return RedirectToAction("Search", "Home");
            }
        }

    }
}