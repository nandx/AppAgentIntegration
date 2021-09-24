using System;
using System.Globalization;
using System.Linq;
using AppAgentIntegration.ClientApi;
using AppAgentIntegration.Dao;
using AppAgentIntegration.Dto;
using AppAgentIntegration.Model;
using AppAgentIntegration.Payload;
using Newtonsoft.Json;

namespace AppAgentIntegration.Service
{
    public class AgentUpdateService
    {
        public void ProcessUpdate()
        {
            Console.WriteLine("===== syncDataAgent =====");
            var agentDao = new AgentDataNewDao();
            var list = agentDao.GetListAgentUpdate();
            if (list == null)
                return;
            
            for (var i = 0; i < list.Count; i++)
            {
                var data = list[i];
                var payeeid = int.Parse(data.PayeeID,
                    NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite); // returns 100

                var logDao = new LogAgentDataNewDao();
                var logAgent = logDao.FindById(payeeid);

                if (logAgent != null && data.UpdatedDate != null
                                     && logAgent.CREATEDDATE != null
                                     && data.UpdatedDate > logAgent.CREATEDDATE)
                {
                    Console.WriteLine("AgentDataUpdateSyncService || payeeid : " + payeeid);

                    UpdateAgent(data, logAgent.IDPROFILE);
                }
            }
        }
        
        public void UpdateAgent(AgentDataNew agent, int? idprofile)
        {
            if (idprofile == null)
                return;

            var payeeid = int.Parse(agent.PayeeID,
                NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite); // returns 100

            var dao = new LogAgentDataUpdateDao();
            var data = dao.FindById(payeeid);
            if (data != null)
            {
                //update
                var requestdata = RequestBody(agent);
                var taspenApi = new TaspenApi();
                var responsedata = taspenApi.UpdateAgent(requestdata, (int) idprofile);

                data.REQUESTDATA = requestdata;
                data.RESPONSEDATA = responsedata;
                data.CREATEDDATE = DateTime.Now;
                dao.Update(data);
            }
            else
            {
                //create new
                var requestdata = RequestBody(agent);
                var taspenApi = new TaspenApi();
                var responsedata = taspenApi.UpdateAgent(requestdata, (int) idprofile);

                data = new LogAgentDataUpdate();
                data.IDAGENT = payeeid;
                data.REQUESTDATA = requestdata;
                data.RESPONSEDATA = responsedata;
                data.CREATEDDATE = DateTime.Now;
                dao.Create(data);
            }

            LogAgentDataUpdateHistoryDao historyDao = new LogAgentDataUpdateHistoryDao();
            var history = new LogAgentDataUpdateHistory();
            history.IDAGENT = data.IDAGENT;
            history.REQUESTDATA = data.REQUESTDATA;
            history.RESPONSEDATA = data.RESPONSEDATA;
            history.CREATEDDATE = DateTime.Now;
            historyDao.Create(history);
        }
        
        private string RequestBody(AgentDataNew agent)
        {
            var nameAgentPosition = agent.Title;
            var taspenApi = new TaspenApi();

            var resultAgentPosition = taspenApi.SearchAgentPosition(nameAgentPosition);
            var agentPositionDto = JsonConvert.DeserializeObject<AgentPositionDto>(resultAgentPosition);
            if (agentPositionDto == null)
                return null;

            if (agentPositionDto.PositionMetaDto?.PositionMetaPaginationDto == null
                || agentPositionDto.PositionMetaDto.PositionMetaPaginationDto.Total == 0)
                return null;

            var positionAgentDto = agentPositionDto.ListAgentPosition.FirstOrDefault();
            if (positionAgentDto == null)
                return null;
            
            var licenseExpireAt = agent.ExpiryDate != null
                ? agent.ExpiryDate.GetValueOrDefault().ToString("yyyy-MM-dd")
                : "-";
            // var licenseExpireAt = agent.ExpiryDate;
            var licenseNumber = agent.LicenseID != null && agent.LicenseID.Trim().Length > 0 
                ? agent.LicenseID : "-";

            var licenseStatus = "";
            if (agent.EmployeeStatus != null && agent.EmployeeStatus.StartsWith("A") && licenseNumber != null &&
                !"-".Equals(licenseNumber) && !"-".Equals(licenseExpireAt))
                licenseStatus = "valid";

            var dto = new AgentNewPayloadDTO();
            dto.instanceId = 2; //TODO (saat ini di set default = 2)
            dto.code = agent.PayeeID;
            dto.name = agent.Name;
            // dto.address = agent.Address;
            // address = agent.Address,
            //20 Sept 2021: Address diganti GA Office Name
            dto.address = agent.GAOfficeName != null && agent.GAOfficeName.Trim().Length > 0 ? agent.GAOfficeName : "";
            dto.phone = agent.Phone != null && agent.Phone.Trim().Length > 0 ? agent.Phone : "-";
            dto.email = agent.EmailAddress != null && agent.EmailAddress.Trim().Length > 0 ? agent.EmailAddress : "-";
            dto.positionId = positionAgentDto.Id; //TODO
            dto.licenseNumber = licenseNumber;
            dto.licenseDate = "-";
            dto.licenseExpireAt = licenseExpireAt;
            dto.licenseStatus = licenseStatus;
            
            Console.WriteLine("licenseExpireAt --> "+licenseExpireAt);
            Console.WriteLine("code --> "+dto.code);

            var payload = new AgentNewPayload();
            payload.agentProfile = dto;

            var jsonString = JsonConvert.SerializeObject(payload);
            return jsonString;
        }
        
    }
}