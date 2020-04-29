using eventApi.DataLayer;
using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.BusinessLayer
{
    public class BusinessAuth : IBusinessAuth
    {
        IRepositoryAuthentication _irep = null;

        public BusinessAuth():this(new Repository())
        {

        }

        public BusinessAuth(IRepositoryAuthentication irep)
        {
            _irep = irep;
        }

        public bool Logout(string uname, string pass)
        {
            bool isuser =false;
    
           
                HttpContext current = HttpContext.Current;
            current.Session.Remove("LOGGEDIN");
            if (current.Session["LOGGEDIN"] == null)
            {
                isuser = true;
            }
            
            return isuser;
        }

        public bool VerifyLogin(string uname, string pass)
        {
            bool isuser = false;
            if (_irep.VerifyLogin(uname, pass))
            {
                isuser = true;
                HttpContext current = HttpContext.Current;
                current.Session["LOGGEDIN"] = uname.ToString();
                //current.Session.Timeout = 1;
            }
            else
            {
                isuser = false;
                HttpContext current = HttpContext.Current;
                current.Session["LOGGEDIN"] = "NOUSER";
            }
            return isuser;
        }

        public bool isLoggedIn()
        {
            bool logged = false;
            HttpContext current = HttpContext.Current;
            if ((current.Session["LOGGEDIN"])!= null)
            {
                logged = true;
            }
            else
            {
                logged = false;
            }
               
           
            return logged;
        }

        public void createUser(User user)
        {
            _irep.createUser(user);
        }


    }
}