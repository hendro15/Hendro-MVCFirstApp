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
        public NpgsqlConnection connection()
        {
            string con = "Server=localhost;Port=5432;User Id=Sonic;Password=sonic;Database=new_irci";
            NpgsqlConnection objConn = new NpgsqlConnection(con);

            return objConn;
        }
    }
}