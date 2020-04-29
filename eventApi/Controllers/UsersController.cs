using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eventApi.Controllers
{
   

    public class UsersController : ApiController
    {
        List<User> user = new List<User>();
        public UsersController()
        {
            user.Add(new User { fname = "Scott", lname = "Colas", Id = 0 });
            user.Add(new User { fname = "Scott1", lname = "Colas1", Id = 1 });
            user.Add(new User { fname = "Scott2", lname = "Colas2", Id = 2 });
        }

        [Route("api/Users/Getfnames")]
        [HttpGet]
        public List<string> Getfnames()
        {
            List<string> output = new List<string>();
            foreach(var i in user)
            {
                output.Add(i.fname);
            }
            return output;
                  
        }

        // GET: api/Users
        public List<User> Get()
        {
            return user;
        }

        // GET: api/Users/5
        public User Get(int id)
        {
            return user.Where(x => x.Id == id).FirstOrDefault(); 
        }

        // POST: api/Users
        public void Post(User value)
        {
            user.Add(value);
        }

  

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}
