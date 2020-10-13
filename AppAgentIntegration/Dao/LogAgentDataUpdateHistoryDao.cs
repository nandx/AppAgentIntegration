using System.Data;
using System.Data.SqlClient;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class LogAgentDataUpdateHistoryDao
    {
        public void Create(LogAgentDataUpdateHistory entity)
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "INSERT INTO LOG_AGENTDATAUPDATEHISTORY(IDAGENT, REQUESTDATA,RESPONSEDATA) VALUES(@p_idagent, @p_requestdata, @p_responsedata)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.Parameters.Add("@p_idagent", SqlDbType.Int).Value = entity.IDAGENT;
                cmd.Parameters.Add("@p_requestdata", SqlDbType.VarChar).Value = entity.REQUESTDATA;  
                cmd.Parameters.Add("@p_responsedata", SqlDbType.VarChar).Value = entity.RESPONSEDATA;  
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}