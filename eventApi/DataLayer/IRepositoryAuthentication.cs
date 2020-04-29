using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApi.DataLayer
{
    public interface IRepositoryAuthentication
    {
        bool VerifyLogin(string name, string pwd);
        void createUser(User user);
    }
}
