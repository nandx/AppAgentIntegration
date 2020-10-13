using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class LogAgentDataNewDao
    {
        public void Create(LogAgentDataNew entity)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "INSERT INTO LOG_AGENTDATANEW(IDAGENT, REQUESTDATA,RESPONSEDATA,IDPROFILE) VALUES(@p_idagent, @p_requestdata,@p_responsedata,@p_idprofile)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                //open connection
                connection.Open();
                
                cmd.Parameters.Add("@p_idagent", SqlDbType.Int).Value = entity.IDAGENT;
                cmd.Parameters.Add("@p_requestdata", SqlDbType.VarChar).Value = entity.REQUESTDATA;  
                cmd.Parameters.Add("@p_responsedata", SqlDbType.VarChar).Value = entity.RESPONSEDATA;  
                cmd.Parameters.Add("@p_idprofile", SqlDbType.Int).Value = entity.IDPROFILE;  
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery(); 
                
                connection.Close();

                
            }

        }

        public LogAgentDataNew FindById(int id)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    //retrieve the SQL Server instance version
                    string query = @"SELECT e.IDAGENT, e.REQUESTDATA, e.RESPONSEDATA, e.IDPROFILE, e.CREATEDDATE
                    FROM LOG_AGENTDATANEW e
                    WHERE e.IDAGENT = @p_id ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    
                    var pid = new SqlParameter("p_id", SqlDbType.Int);
                    pid.Value = id;
                    cmd.Parameters.Add(pid);

                    //open connection
                    connection.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    var result = new List<LogAgentDataNew>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            var data = new LogAgentDataNew();
                            data.IDAGENT = dr.GetInt32(0);
                            data.REQUESTDATA = SafeGetString(dr, 1);
                            data.RESPONSEDATA = SafeGetString(dr, 2);
                            data.IDPROFILE = SafeGetInt(dr, 3);
                            data.CREATEDDATE = SafeGetDatetime(dr, 4);
                            

                            result.Add(data);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    connection.Close();
                    if(result.Count > 0)
                        return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }            
            return null;
        }
        
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
       
        public static DateTime? SafeGetDatetime(SqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return null;
        }
        
        public static int? SafeGetInt(SqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return null;
        }
    }
}