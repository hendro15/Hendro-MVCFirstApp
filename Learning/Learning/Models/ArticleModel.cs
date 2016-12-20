using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace Learning.Models
{

    public class ArticleModel
    {
        public int idArtikel { get; set; }
        public String judulArtikel { get; set; }
        public int tahunArtikel { get; set; }
        public int penulisArtikel { get; set; }
        public int jumlahCitasi { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime tahun { get; set; }

        public int statusArtikel { get; set; }
    }

}