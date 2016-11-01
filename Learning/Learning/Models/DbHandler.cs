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
        public String messageUpdate { get; set; }

        private ArticleModel articleModel;
        private AuthorViewModel authorModel;
        public List<ArticleModel> listArtikel = new List<ArticleModel>();
        public List<AuthorViewModel> searchResult = new List<AuthorViewModel>();
        public List<string> penulis = new List<string>();

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

        public void searchAuthor(string key)
        {
            string[] keywords = key.Split(' ');

            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                //search in db table penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT akun_penulis.id_akun_penulis, akun_penulis.nama_lengkap, akun_penulis.afiliasi FROM irci.akun_penulis WHERE akun_penulis.nama_lengkap LIKE '%" + word + "%'";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            this.authorModel = new AuthorViewModel();
                            authorModel.UserId = int.Parse(reader[0].ToString());
                            authorModel.Fullname = reader[1].ToString();
                            authorModel.Affiliasi = reader[2].ToString();

                            searchResult.Add(authorModel);
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
                        authorName = reader[1].ToString();
                        authorEmail = reader[2].ToString();
                        authorAf = reader[4].ToString();
                    }
                }

                string query2 = "SELECT nama_penulis FROM irci.penulis WHERE id_akun_penulis = " + id;
                using (NpgsqlCommand command = new NpgsqlCommand(query2, objConn))
                {
                    this.authorModel = new AuthorViewModel();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nama = reader[0].ToString();
                        penulis.Add(nama);
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

                //search in db table penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT penulis.id_penulis, penulis.nama_penulis FROM irci.penulis WHERE penulis.nama_penulis LIKE '%" + word + "%' AND penulis.id_akun_penulis < 1";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            this.authorModel = new AuthorViewModel();
                            authorModel.UserId = int.Parse(reader[0].ToString());
                            authorModel.Fullname = reader[1].ToString();
                            authorModel.Affiliasi = "Afiliasi belum diisi";

                            searchResult.Add(authorModel);
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

        public void mergeAccount(int idAuthor, int idPenulis)
        {
            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                String query = "UPDATE irci.penulis SET id_akun_penulis = " + idAuthor + " WHERE id_penulis = " + idPenulis;

                NpgsqlCommand command = new NpgsqlCommand(query, objConn);
                command.ExecuteNonQuery();

                messageUpdate = "Merge Account for idPenulis " + idPenulis + " = success";

                System.Diagnostics.Debug.WriteLine(messageUpdate);
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