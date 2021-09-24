using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Dao
{
    public class AgentDataNewVersion2Dao
    {

        public List<AgentDataNew> GetListAgentNew()
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    var query = @"SELECT e.PayeeID, e.Name, e.JoinDate, e.Title, e.EmailAddress,
                    e.Address, e.RecruitBy, e.RecruiterName, e.AMCode, e.AMName, 
                    e.SAMCode, e.SAMName, e.ADCode, e.ADNAme, e.GAOfficeCode, 
                    e.GAOfficeName, e.PERSON_ID, e.LicenseID, e.Phone, e.EmployeeStatus,
                    e.TerminationComments, e.AccountNo, e.BankCode, e.ExpiryDate, e.TerminationDate,
                    e.CreatedDate, e.UpdatedDate, e.DeletedDate 
                    FROM AGENTDATANEW e
                    WHERE e.CreatedDate IS NOT NULL AND e.DeletedDate IS NULL 
                    ORDER BY e.CreatedDate DESC ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //open connection
                    connection.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    

                    //check if there are records
                    List<AgentDataNew> result = new List<AgentDataNew>();
                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            AgentDataNew data = new AgentDataNew();
                            
                            data.PayeeID = SafeGetString(dr, 0);
                            data.Name =SafeGetString(dr, 1);
                            data.JoinDate = SafeGetDatetime(dr,2);
                            data.Title = SafeGetString(dr, 3);
                            data.EmailAddress = SafeGetString(dr, 4);
                            data.Address = SafeGetString(dr, 5);
                            data.RecruitBy = SafeGetString(dr, 6);
                            data.RecruiterName = SafeGetString(dr, 7);
                            data.AMCode = SafeGetString(dr, 8);
                            data.AMName = SafeGetString(dr, 9);
                            data.SAMCode = SafeGetString(dr, 10);
                            data.SAMName = SafeGetString(dr, 11);
                            data.ADCode = SafeGetString(dr, 12);
                            data.ADNAme = SafeGetString(dr, 13);
                            data.GAOfficeCode = SafeGetString(dr, 14);
                            data.GAOfficeName = SafeGetString(dr, 15);
                            data.PERSON_ID =SafeGetString(dr, 16);
                            data.LicenseID = SafeGetString(dr,17);
                            data.Phone = SafeGetString(dr, 18);
                            data.EmployeeStatus = SafeGetString(dr, 19);
                            data.TerminationComments = SafeGetString(dr, 20);
                            data.AccountNo = SafeGetString(dr, 21);
                            data.BankCode = SafeGetString(dr, 22);
                            data.ExpiryDate = SafeGetDatetime(dr, 23);
                            data.TerminationDate = SafeGetDatetime(dr, 24);
                            data.CreatedDate = SafeGetDatetime(dr, 25);
                            data.UpdatedDate = SafeGetDatetime(dr, 26);
                            data.DeletedDate = SafeGetDatetime(dr, 27);

                            result.Add(data);
                        }
                    }
                   

                    //close data reader
                    dr.Close();

                    //close connection
                    connection.Close();
                    if(result.Count > 0)
                        return result;
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
            
            return null;
        }
        
        public List<AgentDataNew> GetListAgentUpdate()
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    const string query = @"SELECT e.PayeeID, e.Name, e.JoinDate, e.Title, e.EmailAddress,
                    e.Address, e.RecruitBy, e.RecruiterName, e.AMCode, e.AMName, 
                    e.SAMCode, e.SAMName, e.ADCode, e.ADNAme, e.GAOfficeCode, 
                    e.GAOfficeName, e.PERSON_ID, e.LicenseID, e.Phone, e.EmployeeStatus,
                    e.TerminationComments, e.AccountNo, e.BankCode, e.ExpiryDate, e.TerminationDate,
                    e.CreatedDate, e.UpdatedDate, e.DeletedDate 
                    FROM AGENTDATANEW e
                    WHERE e.CreatedDate IS NOT NULL AND e.UpdatedDate IS NOT NULL AND e.DeletedDate IS NULL
                    ORDER BY e.UpdatedDate DESC ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //open connection
                    connection.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    

                    //check if there are records
                    List<AgentDataNew> result = new List<AgentDataNew>();
                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            AgentDataNew data = new AgentDataNew();
                            
                            data.PayeeID = SafeGetString(dr, 0);
                            data.Name =SafeGetString(dr, 1);
                            data.JoinDate = SafeGetDatetime(dr,2);
                            data.Title = SafeGetString(dr, 3);
                            data.EmailAddress = SafeGetString(dr, 4);
                            data.Address = SafeGetString(dr, 5);
                            data.RecruitBy = SafeGetString(dr, 6);
                            data.RecruiterName = SafeGetString(dr, 7);
                            data.AMCode = SafeGetString(dr, 8);
                            data.AMName = SafeGetString(dr, 9);
                            data.SAMCode = SafeGetString(dr, 10);
                            data.SAMName = SafeGetString(dr, 11);
                            data.ADCode = SafeGetString(dr, 12);
                            data.ADNAme = SafeGetString(dr, 13);
                            data.GAOfficeCode = SafeGetString(dr, 14);
                            data.GAOfficeName = SafeGetString(dr, 15);
                            data.PERSON_ID =SafeGetString(dr, 16);
                            data.LicenseID = SafeGetString(dr,17);
                            data.Phone = SafeGetString(dr, 18);
                            data.EmployeeStatus = SafeGetString(dr, 19);
                            data.TerminationComments = SafeGetString(dr, 20);
                            data.AccountNo = SafeGetString(dr, 21);
                            data.BankCode = SafeGetString(dr, 22);
                            data.ExpiryDate = SafeGetDatetime(dr, 23);
                            data.TerminationDate = SafeGetDatetime(dr, 24);
                            data.CreatedDate = SafeGetDatetime(dr, 25);
                            data.UpdatedDate = SafeGetDatetime(dr, 26);
                            data.DeletedDate = SafeGetDatetime(dr, 27);

                            result.Add(data);
                        }
                    }
                   

                    //close data reader
                    dr.Close();

                    //close connection
                    connection.Close();
                    if(result.Count > 0)
                        return result;
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
            
            return null;
        }
        
         public List<AgentDataNew> GetListAgentDelete()
        {
            SqlConnectionStringBuilder builder = ConnectionStringBuilder.GetConnectionStringBuilder();
            try
            {
                //sql connection object
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    var query = @"SELECT e.PayeeID, e.Name, e.JoinDate, e.Title, e.EmailAddress,
                    e.Address, e.RecruitBy, e.RecruiterName, e.AMCode, e.AMName, 
                    e.SAMCode, e.SAMName, e.ADCode, e.ADNAme, e.GAOfficeCode, 
                    e.GAOfficeName, e.PERSON_ID, e.LicenseID, e.Phone, e.EmployeeStatus,
                    e.TerminationComments, e.AccountNo, e.BankCode, e.ExpiryDate, e.TerminationDate,
                    e.CreatedDate, e.UpdatedDate, e.DeletedDate 
                    FROM AGENTDATANEW e
                    WHERE e.CreatedDate IS NOT NULL AND e.UpdatedDate IS NOT NULL AND e.DeletedDate IS NOT NULL
                    ORDER BY e.DeletedDate DESC ;";
                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //open connection
                    connection.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();

                    

                    //check if there are records
                    List<AgentDataNew> result = new List<AgentDataNew>();
                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            AgentDataNew data = new AgentDataNew();
                            
                            data.PayeeID = SafeGetString(dr, 0);
                            data.Name =SafeGetString(dr, 1);
                            data.JoinDate = SafeGetDatetime(dr,2);
                            data.Title = SafeGetString(dr, 3);
                            data.EmailAddress = SafeGetString(dr, 4);
                            data.Address = SafeGetString(dr, 5);
                            data.RecruitBy = SafeGetString(dr, 6);
                            data.RecruiterName = SafeGetString(dr, 7);
                            data.AMCode = SafeGetString(dr, 8);
                            data.AMName = SafeGetString(dr, 9);
                            data.SAMCode = SafeGetString(dr, 10);
                            data.SAMName = SafeGetString(dr, 11);
                            data.ADCode = SafeGetString(dr, 12);
                            data.ADNAme = SafeGetString(dr, 13);
                            data.GAOfficeCode = SafeGetString(dr, 14);
                            data.GAOfficeName = SafeGetString(dr, 15);
                            data.PERSON_ID =SafeGetString(dr, 16);
                            data.LicenseID = SafeGetString(dr,17);
                            data.Phone = SafeGetString(dr, 18);
                            data.EmployeeStatus = SafeGetString(dr, 19);
                            data.TerminationComments = SafeGetString(dr, 20);
                            data.AccountNo = SafeGetString(dr, 21);
                            data.BankCode = SafeGetString(dr, 22);
                            data.ExpiryDate = SafeGetDatetime(dr, 23);
                            data.TerminationDate = SafeGetDatetime(dr, 24);
                            data.CreatedDate = SafeGetDatetime(dr, 25);
                            data.UpdatedDate = SafeGetDatetime(dr, 26);
                            data.DeletedDate = SafeGetDatetime(dr, 27);

                            result.Add(data);
                        }
                    }
                   

                    //close data reader
                    dr.Close();

                    //close connection
                    connection.Close();
                    if(result.Count > 0)
                        return result;
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


    }
}