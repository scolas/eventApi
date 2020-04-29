using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.Models
{
    public class Invite
    {

        public int id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public int createrId { get; set; }
        public int eventId { get; set; }
        public String status { get; set; }
        public String inviteusername { get; set; }
        public String invitetitle { get; set; }
        public String invitedate { get; set; }
        public Event events { get; set; }

       
    }
}