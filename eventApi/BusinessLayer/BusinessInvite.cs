using eventApi.DataLayer;
using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.BusinessLayer
{
    public class BusinessInvite : IBusinessInvite
    {
        IRepositoryInvite _irep = null;

        public BusinessInvite() : this(new Repository())
        {

        }
        public BusinessInvite(IRepositoryInvite irep)
        {
            _irep = irep;
        }

        public List<Invite> getInvite()
        {
            List<Invite> i = _irep.getInvite();
            return i;
        }

        public List<Invite> getInvite(string username)
        {
            List<Invite> i = _irep.getInvite(username);
            return i;
        }

        public bool acceptInvite(int id)
        {
            Boolean i = _irep.acceptInvite(id);
            return i;
        }

        public bool denyInvite(int id)
        {
            Boolean i = _irep.denyInvite(id);
            return i;
        }
    }
}