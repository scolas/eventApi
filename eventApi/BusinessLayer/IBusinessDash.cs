using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApi.BusinessLayer
{
    interface IBusinessDash
    {
        List<Employee> employees();
    }
}
