using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class LogAgentDataUpdateDao
    {
         public LogAgentDataUpdate FindById(int id)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    string query = @"SELECT e.IDAGENT, e.REQUESTDATA, e.RESPONSEDATA, e.CREATEDDATE
                    FROM LOG_AGENTDATAUPDATE e
                    WHERE e.IDAGENT = @p_id ;";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    
                    var pid = new SqlParameter("p_id", SqlDbType.Int);
                    pid.Value = id;
                    cmd.Parameters.Add(pid);

                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    var result = new List<LogAgentDataUpdate>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            var data = new LogAgentDataUpdate();
                            data.IDAGENT = dr.GetInt32(0);
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


         public void Create(LogAgentDataUpdate entity)
         {
             SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
             using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
             {
                 string sql = "INSERT INTO LOG_AGENTDATAUPDATE(IDAGENT, REQUESTDATA,RESPONSEDATA) VALUES(@p_idagent, @p_requestdata, @p_responsedata)";
                 SqlCommand cmd = new SqlCommand(sql, connection);
                 connection.Open();
                 cmd.Parameters.Add("@p_idagent", SqlDbType.Int).Value = entity.IDAGENT;
                 cmd.Parameters.Add("@p_requestdata", SqlDbType.VarChar).Value = entity.REQUESTDATA;  
                 cmd.Parameters.Add("@p_responsedata", SqlDbType.VarChar).Value = entity.RESPONSEDATA;  
                 cmd.Parameters.Add("@p_createddate", SqlDbType.DateTime).Value = DateTime.Now;  

                 cmd.CommandType = CommandType.Text;
                 cmd.ExecuteNonQuery();
                 connection.Close();
             }
         }
         
         public void Update(LogAgentDataUpdate entity)
         {
             SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
             using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
             {
                 string sql = "UPDATE LOG_AGENTDATAUPDATE SET REQUESTDATA = @p_requestdata, RESPONSEDATA = @p_responsedata, CREATEDDATE = @p_createddate WHERE IDAGENT = @p_idagent ";

                 SqlCommand cmd = new SqlCommand(sql, connection);
                 connection.Open();
                 cmd.Parameters.Add("@p_idagent", SqlDbType.Int).Value = entity.IDAGENT;
                 cmd.Parameters.Add("@p_requestdata", SqlDbType.VarChar).Value = entity.REQUESTDATA;  
                 cmd.Parameters.Add("@p_responsedata", SqlDbType.VarChar).Value = entity.RESPONSEDATA;  
                 cmd.Parameters.Add("@p_createddate", SqlDbType.DateTime).Value = DateTime.Now;  

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
             if (!reader.IsDBNull(colIndex))
                 return reader.GetDateTime(colIndex);
             return null;
         }
         
    }
}