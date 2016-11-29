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
        public String penulisArtikel { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime tahun { get; set; }

        public int statusArtikel { get; set; }
    }

    public class Article
    {
        private ArticleModel articleModel;
        private DbHandler dbHandler;
        private NpgsqlConnection con;
        private NpgsqlCommand command;
        private NpgsqlDataReader reader;
        private List<ArticleModel> list;

        public List<ArticleModel> artikelList()
        {
            list = new List<ArticleModel>();
            dbHandler = new DbHandler();

            try
            {
                con = dbHandler.connection();
                con.Open();

                string query = "SELECT * FROM irci.artikel";

                using (command = new NpgsqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articleModel = new ArticleModel();
                        articleModel.idArtikel = int.Parse(reader[0].ToString());
                        articleModel.judulArtikel = reader[1].ToString();
                        articleModel.penulisArtikel = reader[2].ToString();
                        articleModel.tahun = DateTime.Parse(reader[3].ToString());
                        articleModel.statusArtikel = int.Parse(reader[4].ToString());
                        list.Add(articleModel);
                    }
                }
                con.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
            
            return list;
        }
    }
}