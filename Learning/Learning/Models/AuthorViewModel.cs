using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learning.Models
{
    public class AuthorAllModel
    {
        public SearchModel searchModel { get; set; }
        public AuthorViewModel authorModel { get; set; }
    }

    public class AuthorViewModel
    {
        public int UserId { get; set; }
        public String Fullname { get; set; }
        public String email { get; set; }
        public string Affiliasi { get; set; }

    }
}