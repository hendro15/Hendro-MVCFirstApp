using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learning.Models
{
    
    public class ArticleModel
    {
        public int idArtikel { get; set; }
        public String judulArtikel { get; set; }
        public String penulisArtikel { get; set; }
        public String tahun { get; set; }
        public int statusArtikel { get; set; }
    }

    public class ArticleRecords
    {
        public List<ArticleModel> artikelList { get; set; }
        //public List<ArticleModel> artikelList = new List<ArticleModel>();
        //public ArrayList artikel { get; set; }
    }
}