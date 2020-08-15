using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Utlities
{
    public class DataCommon
    {
        protected static IDbConnection DbCon { get { return new DbHelper().Connection; } }
    }
}
