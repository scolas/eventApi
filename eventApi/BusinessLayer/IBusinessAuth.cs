using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApi.BusinessLayer
{
    public interface IBusinessAuth
    {
        bool VerifyLogin(string uname, string pass);
        bool Logout(string uname, string pass);
        void createUser(User user);
        bool isLoggedIn();
    }
}
