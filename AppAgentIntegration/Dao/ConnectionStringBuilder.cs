using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AppAgentIntegration.Dao
{
    public class ConnectionStringBuilder
    {
        public static SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            var appSetting = ConfigurationSettings.AppSettings["Title"];
            //Console.WriteLine("appSetting :: "+appSetting);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.DataSource = "localhost";   
            //builder.UserID = "sa";              
            //builder.Password = "admin*123";      
            //builder.InitialCatalog = "individu300920";
            
            builder.DataSource = ConfigurationSettings.AppSettings["DataSource"];   
            builder.UserID = ConfigurationSettings.AppSettings["UserID"];              
            builder.Password = ConfigurationSettings.AppSettings["Password"];    
            builder.InitialCatalog = ConfigurationSettings.AppSettings["InitialCatalog"]; 
            
            return builder;
        }
    }
}