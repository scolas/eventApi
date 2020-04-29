using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.Models
{
    public class Day
    {
       public int day { get; set; }
       public string name { get; set; }
       public int month { get; set; }
       public int year { get; set; }


        public List<Event> events { get; set; }


    }
}