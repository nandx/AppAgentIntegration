using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class SysParamDao
    {
        public SysParam GetParamValue(string paramname)
        {
             SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    
                    //retrieve the SQL Server instance version
                    string query = @"SELECT e.PARAMID, e.PARAMNAME, e.PARAMVALUE
                    FROM INTTASPENSYSPARAM e
                    WHERE e.PARAMNAME = @p_paramname ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);
                    
                    var pname = new SqlParameter("p_paramname", SqlDbType.Char);
                    pname.Value = paramname;
                    cmd.Parameters.Add(pname);

                    //open connection
                    connection.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    var result = new List<SysParam>();
                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            var data = new SysParam();
                            data.PARAMNAME =SafeGetString(dr, 1);
                            data.PARAMVALUE = SafeGetString(dr,2);
                            
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
    }
}