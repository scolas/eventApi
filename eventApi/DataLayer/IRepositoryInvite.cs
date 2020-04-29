using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApi.DataLayer
{
    public interface IRepositoryInvite
    {
        List<Invite> getInvite();
        List<Invite> getInvite(string username);

        bool acceptInvite(int id);
        bool denyInvite(int id);
    }
}
