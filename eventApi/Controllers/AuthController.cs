using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eventApi.Controllers
{
    public class AuthController : ApiController
    {
        // GET: api/Auth
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        public void Post([FromBody]string value)
        {
        }



        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
