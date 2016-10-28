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

        public void readAuthor(string name)
        {
            try
            {
                NpgsqlConnection objConn = new NpgsqlConnection(con);
                objConn.Open();

                string query = "SELECT * FROM irci.akun_penulis WHERE nama_lengkap = '" + name + "'";
                using(NpgsqlCommand command = new NpgsqlCommand(query, objConn))
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
            catch(Exception msg)
            {
                System.Diagnostics.Debug.WriteLine(msg.ToString());
                throw;
            }
        }

    }
}