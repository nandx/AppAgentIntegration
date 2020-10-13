using System.Data.SqlClient;

namespace AppAgentIntegration.Dao
{
    public class ConnectionStringBuilder
    {
        public static SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";   // update me
            builder.UserID = "sa";              // update me
            builder.Password = "admin*123";      // update me
            builder.InitialCatalog = "individu300920";
            return builder;
        }
    }
}