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
        private List<ArticleModel> artikelList = new List<ArticleModel>();
        private List<ArticleModel> prosesArtikelList;
        private ArticleModel articleModel;

        // GET: Admin
        public ActionResult Article()
        {
            
            if (Session["LogedUserFullname"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public ActionResult ArticleProcess(List<ArticleModel> artikelList)
        {
            this.prosesArtikelList = new List<ArticleModel>();
            

            for (int i = 0; i < 10; i++)
            {
                this.articleModel = new ArticleModel();
                articleModel.idArtikel = int.Parse(artikelList[i].idArtikel.ToString());
                articleModel.judulArtikel = artikelList[i].judulArtikel.ToString();

                prosesArtikelList.Add(articleModel);
            }
            return View();
        }
    }
}