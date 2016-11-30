using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

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
        public List<string> penulis { get; set; }
    }

    public class SearchAuthor
    {
        public string key { get; set; }
        public List<AuthorModel> authorList { get; set; }
    }

    public class Author
    {
        private AuthorModel authorModel;
        private DbHandler dbHandler;
        private NpgsqlConnection con;
        private NpgsqlCommand command;
        private NpgsqlDataReader reader;
        private List<AuthorModel> list;

        public AuthorModel login(string email, string pass)
        {
            dbHandler = new DbHandler();
            authorModel = new AuthorModel();
            string query = "SELECT * FROM irci.akun_penulis WHERE email = '" + email + "' and password = '" + pass + "'";

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
                        authorModel.fullname = reader[1].ToString();
                        authorModel.email = reader[2].ToString();
                        authorModel.password = reader[3].ToString();
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
            List<String> authPenulis = new List<String>();
            dbHandler = new DbHandler();
            authorModel = new AuthorModel();
            string query = "SELECT * FROM irci.akun_penulis WHERE id_akun_penulis = " + id;
            string query2 = "SELECT nama_penulis FROM irci.penulis WHERE id_akun_penulis = " + id;

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
                        authorModel.fullname = reader[1].ToString();
                        authorModel.email = reader[2].ToString();
                        authorModel.password = reader[3].ToString();
                        authorModel.afiliasi = reader[4].ToString();
                    }
                }


                using (command = new NpgsqlCommand(query2, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string nama = reader[0].ToString();
                        authPenulis.Add(nama);
                    }
                }

                authorModel.penulis = authPenulis;

                con.Close();

            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }

            return authorModel;
        }

        public List<AuthorModel> researcherList(string key)
        {
            list = new List<AuthorModel>();
            dbHandler = new DbHandler();

            string[] keywords = key.Split(' ');
            try
            {
                con = dbHandler.connection();
                con.Open();

                //search in db table penulis
                foreach (string word in keywords)
                {
                    string query = "SELECT akun_penulis.id_akun_penulis, akun_penulis.nama_lengkap, akun_penulis.afiliasi FROM irci.akun_penulis WHERE lower(akun_penulis.nama_lengkap) LIKE lower('%" + word + "%')";

                    using (command = new NpgsqlCommand(query, con))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            this.authorModel = new AuthorModel();
                            authorModel.userId = int.Parse(reader[0].ToString());
                            authorModel.fullname = reader[1].ToString();
                            authorModel.afiliasi = reader[2].ToString();

                            list.Add(authorModel);
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

            return list;
        }

        public void merge(int idAuthor, int idPenulis)
        {
            dbHandler = new DbHandler();
            try
            {
                con = dbHandler.connection();
                con.Open();

                String query = "UPDATE irci.penulis SET id_akun_penulis = " + idAuthor + " WHERE id_penulis = " + idPenulis;

                command = new NpgsqlCommand(query, con);
                command.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }
    }

}