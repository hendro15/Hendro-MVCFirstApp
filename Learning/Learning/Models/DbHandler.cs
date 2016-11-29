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
        string con = "Server=localhost;Port=5432;User Id=Sonic;Password=sonic;Database=our_irci";

        public NpgsqlConnection connection()
        {
            string con = "Server=localhost;Port=5432;User Id=Sonic;Password=sonic;Database=our_irci";
            NpgsqlConnection objConn = new NpgsqlConnection(con);

            return objConn;
        }

        //public void searchProfile(string key)
        //{
        //    string[] keywords = key.Split(' ');

        //    try
        //    {
        //        NpgsqlConnection objConn = new NpgsqlConnection(con);
        //        objConn.Open();

        //        search in db table penulis
        //        foreach (string word in keywords)
        //        {
        //            string query = "SELECT penulis.id_penulis, penulis.nama_penulis FROM irci.penulis WHERE penulis.nama_penulis LIKE '%" + word + "%' AND penulis.id_akun_penulis < 1";

        //            using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
        //            {
        //                NpgsqlDataReader reader = command.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    this.authorModel = new AuthorViewModel();
        //                    authorModel.userId = int.Parse(reader[0].ToString());
        //                    authorModel.fullname = reader[1].ToString();
        //                    authorModel.afiliasi = "Afiliasi belum diisi";

        //                    searchResult.Add(authorModel);
        //                }
        //            }
        //        }
        //        objConn.Close();
        //    }
        //    catch (Exception msg)
        //    {
        //        System.Diagnostics.Debug.WriteLine(msg.ToString());
        //        throw;
        //    }
        //}

        //public void mergeAccount(int idAuthor, int idPenulis)
        //{
        //    try
        //    {
        //        NpgsqlConnection objConn = new NpgsqlConnection(con);
        //        objConn.Open();

        //        String query = "UPDATE irci.penulis SET id_akun_penulis = " + idAuthor + " WHERE id_penulis = " + idPenulis;

        //        NpgsqlCommand command = new NpgsqlCommand(query, objConn);
        //        command.ExecuteNonQuery();

        //        messageUpdate = "Merge Account for idPenulis " + idPenulis + " = success";

        //        System.Diagnostics.Debug.WriteLine(messageUpdate);
        //    }
        //    catch (Exception msg)
        //    {
        //        System.Diagnostics.Debug.WriteLine(msg.ToString());
        //        throw;
        //    }
        //}

        //    public void readArticle()
        //    {
        //        try
        //        {
        //            NpgsqlConnection objConn = new NpgsqlConnection(con);
        //            objConn.Open();

        //            string query = "SELECT * FROM irci.artikel";

        //            using (NpgsqlCommand command = new NpgsqlCommand(query, objConn))
        //            {
        //                NpgsqlDataReader reader = command.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    this.articleModel = new ArticleModel();
        //                    articleModel.idArtikel = int.Parse(reader[0].ToString());
        //                    articleModel.judulArtikel = reader[1].ToString();
        //                    articleModel.penulisArtikel = reader[2].ToString();
        //                    articleModel.tahun = reader[3].ToString();
        //                    articleModel.statusArtikel = int.Parse(reader[4].ToString());
        //                    articleRecors.artikelList.Add(articleModel);
        //                    listArtikel.Add(articleModel);
        //                }

        //            }
        //        }
        //        catch (Exception msg)
        //        {
        //            System.Diagnostics.Debug.WriteLine(msg.ToString());
        //            throw;
        //        }
        //    }

    }
}