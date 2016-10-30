using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

namespace Learning.Models
{
    public class DbHandler
    {
        public int authorId { get; set; }
        public String authorName { get; set; }
        public String authorEmail { get; set; }
        public String authorPass { get; set; }
        public String authorAf { get; set; }


        private ArticleModel articleModel;
        //private ArticleRecords articleRecors = new ArticleRecords();
        public List<ArticleModel> listArtikel = new List<ArticleModel>();

        public List<string> searchResult = new List<string>();

        private string con = "Server=localhost;Port=5432;User Id=Sonic;Password=sonic;Database=our_irci";

        public void loginAuthor(string email, string pass)
        {
            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                string query = "SELECT * FROM irci.akun_penulis WHERE email = '" + email + "' and password = '" + pass + "'";

                using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        authorId = int.Parse(reader[0].ToString());
                        authorName = reader[1].ToString();
                        authorEmail = reader[2].ToString();
                        authorPass = reader[3].ToString();
                    }
                }
                objConn.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

        public void readAuthor(int id)
        {
            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                string query = "SELECT * FROM irci.akun_penulis WHERE id_akun_penulis = " + id;
                using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        authorId = int.Parse(reader[0].ToString());
                        authorEmail = reader[2].ToString();
                        authorAf = reader[4].ToString();
                    }
                }
                objConn.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

        public void searchProfile(string key)
        {
            string[] keywords = key.Split(' ');

            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                //search in akun_penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT akun_penulis.nama_lengkap FROM irci.akun_penulis WHERE akun_penulis.nama_lengkap LIKE '%" + word + "%'";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            authorName = reader[0].ToString();

                            searchResult.Add(authorName);
                        }
                    }
                }

                //search in penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT penulis.nama_penulis FROM irci.penulis WHERE penulis.nama_penulis LIKE '%" + word + "%'";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            authorName = reader[0].ToString();

                            searchResult.Add(authorName);
                        }
                    }
                }
                objConn.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

        public void readArticle()
        {
            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                string query = "SELECT * FROM irci.artikel";

                using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        this.articleModel = new ArticleModel();
                        articleModel.idArtikel = int.Parse(reader[0].ToString());
                        articleModel.judulArtikel = reader[1].ToString();
                        articleModel.penulisArtikel = reader[2].ToString();
                        articleModel.tahun = reader[3].ToString();
                        articleModel.statusArtikel = int.Parse(reader[4].ToString());
                        //articleRecors.artikelList.Add(articleModel);
                        listArtikel.Add(articleModel);
                    }

                }
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

    }
}