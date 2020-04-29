using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string email { get; set; }
        public string pass { get; set; }
        public string username { get; set; }
    }
}