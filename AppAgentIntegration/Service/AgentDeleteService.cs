using System;
using System.Globalization;
using AppAgentIntegration.ClientApi;
using AppAgentIntegration.Dao;
using AppAgentIntegration.Model;

namespace AppAgentIntegration.Service
{
    public class AgentDeleteService
    {
        public void ProcessDelete()
        {
            var agentDAO = new AgentDataNewDao();
            var list = agentDAO.GetListAgentDelete();
            if (list == null)
                return;
            
            for (var i = 0; i < list.Count; i++)
            {
                var data = list[i];
                var payeeid = int.Parse(data.PayeeID,
                    NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite); // returns 100

                var logDao = new LogAgentDataNewDao();
                var logAgent = logDao.FindById(payeeid);
                if (logAgent != null
                    && data.DeletedDate != null
                    && logAgent.IDPROFILE != null)
                    DeleteData((int) logAgent.IDPROFILE);
            }
        }
        
        
        private void DeleteData(int idprofile)
        {
            Console.WriteLine("===== DeleteData : " + idprofile);
            var dao = new LogAgentDataDeleteDao();
            var data = dao.FindById(idprofile);
            if (data != null)
                return;

            var taspenApi = new TaspenApi();
            var responsedata = taspenApi.DeleteAgent(idprofile);

            data = new LogAgentDataDelete();
            data.IDPROFILE = idprofile;
            data.REQUESTDATA = idprofile.ToString();
            data.RESPONSEDATA = responsedata;
            data.CREATEDDATE = DateTime.Now;
            dao.Create(data);
        }
    }
}