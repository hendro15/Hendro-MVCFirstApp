using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Npgsql;
using System;

namespace Learning.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Fullname")]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class Account
    {
        private DbHandler dbHandler;
        private NpgsqlConnection con;
        private NpgsqlCommand command;
        private NpgsqlDataReader reader;

        public void Register(string email, string password, string fullname)
        {
            this.dbHandler = new DbHandler();
            int idAccount = 0;
            String query1 = "INSERT INTO public.account (username, password) VALUES ('" + email + "','" + password + "')";
            
            try
            {
                con = dbHandler.connection();
                con.Open();

                command = new NpgsqlCommand(query1, con);
                command.ExecuteNonQuery();

                String query2 = "SELECT account.id_account FROM public.account WHERE account.username = '" + email + "'";

                using (command = new NpgsqlCommand(query2, con))
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        idAccount = int.Parse(reader[0].ToString());
                    }
                }

                String query3 = "INSERT INTO public.author (name, id_account, default_account) VALUES ('" + fullname + "','" + idAccount + "',1)";

                command = new NpgsqlCommand(query3, con);

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
