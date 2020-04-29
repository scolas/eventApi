using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.Models
{
    public class Employee
    {
           
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string address { get; set; }

        public string mondaystart { get; set; }
        public string mondayend { get; set; }

        public string tuesdaystart { get; set; }
        public string tuesdayend { get; set; }
        public string wednesdaystart { get; set; }
        public string wednesdayend { get; set; }
        public string thursdaystart { get; set; }
        public string thursdayend { get; set; }
        public string fridaystart { get; set; }
        public string fridayend { get; set; }

        public string saturdaystart { get; set; }
        public string saturdayend { get; set; }
        public string sundaystart { get; set; }
        public string sundayend { get; set; }



    }
}