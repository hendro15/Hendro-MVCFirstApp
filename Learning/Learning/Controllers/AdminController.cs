using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Learning.Models;

namespace Learning.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Article(DbHandler db)
        {
            
            if(Session["LogedUserFullname"] != null)
            {
                //db.readArticle();
                //for (int i = 0; i < int.Parse(db.listArtikel.Count.ToString()); i++)
                //{
                //    ar.artikelList.Add(db.listArtikel[i]);
                //}

                db.readArticle();

                return View(db);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
    }
}