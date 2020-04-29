using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApi.DataLayer
{
   public interface IDataAccess
    {
        //the class that impliments will dicide if these are public or private
        object GetSingleAnswer(string sql);
        DataTable GetManyRowsCols(string sql);

        int InsertUpdateDelete(string sql);


        DataSet DataSetXEQDynamicSql(string sql);

    }
}
