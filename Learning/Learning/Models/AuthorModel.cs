using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

namespace Learning.Models
{
    public class AuthorAllModel
    {
        public SearchAuthor searchAuthor { get; set; }
        public AuthorModel authorModel { get; set; }
    }

    public class AuthorModel
    {
        public int userId { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string afiliasi { get; set; }
        public string password { get; set; }
        public DataTable penulis { get; set; }
    }

    public class SearchAuthor
    {
        public string key { get; set; }
        public DataTable authorList { get; set; }
    }

    public class Author
    {
        private AuthorModel authorModel;
        private ArticleModel articleModel;
        private DbHandler dbHandler;
        private NpgsqlConnection con;
        private NpgsqlCommand command;
        private NpgsqlDataReader reader;
        private DataTable dt;
       
        public AuthorModel login(string email, string pass)
        {
            dbHandler = new DbHandler();
            authorModel = new AuthorModel();
            //string query = "SELECT * FROM public.account WHERE username = '" + email + "' and password = '" + pass + "'";
            string query = "SELECT account.id_account, account.username, account.password, author.name  FROM public.account, public.author WHERE account.username = '" + email + "' AND account.password = '" + pass + "' AND account.id_account = author.id_account";

            try
            {
                con = dbHandler.connection();
                con.Open();

                using (command = new NpgsqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        authorModel.userId = int.Parse(reader[0].ToString());
                        authorModel.email = reader[1].ToString();
                        authorModel.password = reader[2].ToString();
                        authorModel.fullname = reader[3].ToString();
                    }
                }
                con.Close();

            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }

            return authorModel;
        }

        public AuthorModel researcher(int id)
        {
            dbHandler = new DbHandler();
            authorModel = new AuthorModel();
            string query = "SELECT account.username, author.name FROM public.account, public.author WHERE account.id_account = " + id + "AND account.id_account = author.id_account AND author.default_account = 1";
            string query2 = "SELECT author.name FROM public.author WHERE author.id_account = " + id + "AND author.default_account = 0";

            this.dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Nama") });

            try
            {
                con = dbHandler.connection();
                con.Open();

                using (command = new NpgsqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        authorModel.email = reader[0].ToString();
                        authorModel.fullname = reader[1].ToString();
                    }
                }


                using (command = new NpgsqlCommand(query2, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        

                        string nama = reader[0].ToString();
                        dt.Rows.Add(nama);
                    }
                }

                authorModel.penulis = dt;

                con.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }

            return authorModel;
        }

        public DataTable authorList(string key)
        {
            dbHandler = new DbHandler();

            string[] keywords = key.Split(' ');
            try
            {
                con = dbHandler.connection();
                con.Open();

                this.dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("Nama"), new DataColumn("ID Account") });
                int id;

                //search in db table penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT author.id_account, author.id_author, author.name FROM public.author WHERE lower(author.name) LIKE lower('%" + word + "%')";

                    using (command = new NpgsqlCommand(query, con))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                id = 0;
                            }
                            else
                            {
                                id = int.Parse(reader[0].ToString());
                            }
                            int id_author = int.Parse(reader[1].ToString());
                            string fullname = reader[2].ToString();

                            dt.Rows.Add(id_author, fullname, id);
                        }
                    }
                }
                con.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }

            return dt;
        }

        public void merge(int idAccount, int idAuthor)
        {
            dbHandler = new DbHandler();
            try
            {
                con = dbHandler.connection();
                con.Open();

                String query = "UPDATE public.author SET id_account = " + idAccount + ", default_account = 0 WHERE id_author = " + idAuthor;

                command = new NpgsqlCommand(query, con);
                command.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

        //BUATAN KITAH
        public DataTable articleList(int id)
        {
            dbHandler = new DbHandler();
            articleModel = new ArticleModel();
            string query = "SELECT a.id_article, a.tittle, ao.id_author FROM article a LEFT JOIN article_author aa on aa.id_article = a.id_article LEFT JOIN author ao on ao.id_author = aa.id_author WHERE ao.id_author = " + id + "";

            try
            {
                con = dbHandler.connection();
                con.Open();
                this.dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID Artikel"), new DataColumn("Judul Artikel"), new DataColumn("ID Author") });

                using (command = new NpgsqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articleModel.idArtikel = int.Parse(reader[0].ToString());
                        articleModel.judulArtikel = reader[1].ToString();
                        articleModel.tahunArtikel = int.Parse(reader[2].ToString());
                        articleModel.penulisArtikel = int.Parse(reader[3].ToString());
                        
                    }
                }
                dt.Rows.Add(articleModel.idArtikel, articleModel.judulArtikel, articleModel.tahunArtikel, articleModel.penulisArtikel);

                con.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }

            return dt;
        }

        public DataTable citationList(int id)
        {
            dbHandler = new DbHandler();
            articleModel = new ArticleModel();
            string query = "SELECT a.id_article, a.tittle, NULLIF(ar.cited_num, 0) cited_num FROM public.article a LEFT JOIN(SELECT id_source_article, COUNT(*) cited_num FROM public.article_reference ar GROUP BY id_source_article) ar on ar.id_source_article = a.id_article LEFT JOIN article_author aa on aa.id_article = ar.id_source_article WHERE aa.id_author = " + id + "";

            try
            {
                con = dbHandler.connection();
                con.Open();
                this.dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID Artikel"), new DataColumn("Judul Artikel"), new DataColumn("Jumlah Citasi")});

                using (command = new NpgsqlCommand(query, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        articleModel.idArtikel = int.Parse(reader[0].ToString());
                        articleModel.judulArtikel = reader[1].ToString();
                        articleModel.jumlahCitasi = int.Parse(reader[2].ToString());
                    }
                }
                dt.Rows.Add(articleModel.idArtikel, articleModel.judulArtikel, articleModel.jumlahCitasi);

                con.Close();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }
            return dt;
        }
        
   }

}