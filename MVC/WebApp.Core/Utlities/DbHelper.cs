using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Core.Utlities
{
    public class DbHelper
    {
        public IDbConnection Connection { get; set; }
        public IDbCommand Command { get; set; }

        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionSql"].ConnectionString;

        public DbHelper()
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
    }
}
