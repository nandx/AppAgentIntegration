using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class LogAgentDataDeleteDao
    {
        public LogAgentDataDelete FindById(int id)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    //retrieve the SQL Server instance version
                    string query = @"SELECT e.IDPROFILE, e.REQUESTDATA, e.RESPONSEDATA, e.CREATEDDATE
                    FROM LOG_AGENTDATADELETE e
                    WHERE e.IDPROFILE = @p_id ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    
                    var pid = new SqlParameter("p_id", SqlDbType.Int);
                    pid.Value = id;
                    cmd.Parameters.Add(pid);

                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    var result = new List<LogAgentDataDelete>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var data = new LogAgentDataDelete();
                            data.IDPROFILE = dr.GetInt32(0);
                            data.REQUESTDATA = SafeGetString(dr, 1);
                            data.RESPONSEDATA = SafeGetString(dr, 2);
                            data.CREATEDDATE = SafeGetDatetime(dr, 3);
                            result.Add(data);
                        }
                    }

                    dr.Close();
                    connection.Close();
                    if(result.Count > 0)
                        return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }            
            return null;
        }
        
        
        public void Create(LogAgentDataDelete entity)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "INSERT INTO LOG_AGENTDATADELETE(IDPROFILE, REQUESTDATA,RESPONSEDATA) VALUES(@p_idprofile, @p_requestdata, @p_responsedata)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.Parameters.Add("@p_idprofile", SqlDbType.Int).Value = entity.IDPROFILE;
                cmd.Parameters.Add("@p_requestdata", SqlDbType.VarChar).Value = entity.REQUESTDATA;  
                cmd.Parameters.Add("@p_responsedata", SqlDbType.VarChar).Value = entity.RESPONSEDATA;  
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
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
    }
}