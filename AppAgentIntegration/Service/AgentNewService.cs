using System;
using System.Collections.Generic;
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
    public class AgentNewService
    {
        public void ProcessData()
        {
            AgentDataNewDao dao = new AgentDataNewDao();
            List<AgentDataNew> list = dao.GetListAgentNew();
            foreach (var data in list)
            {
                var payeeid = int.Parse(data.PayeeID,
                    NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite); // returns 100
                
                var logDao = new LogAgentDataNewDao();
                var logAgent = logDao.FindById(payeeid);
                if (logAgent == null)
                    ProcessData(data);
            }
        }

        private void ProcessData(AgentDataNew agent)
        {
            var requestdata = RequestBody(agent);
            if (requestdata == null)
                return;

            int? idprofile = null;
            var taspenApi = new TaspenApi();
            var responsedata = taspenApi.CreateNewAgent(requestdata);

            if (responsedata != null)
            {
                var agentDto = JsonConvert.DeserializeObject<AgentDto>(responsedata);
                if (agentDto?.listAgentProfile != null) idprofile = agentDto.listAgentProfile.id;
            }

            var payeeid = int.Parse(agent.PayeeID, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);

            var data = new LogAgentDataNew()
            {
                IDAGENT = payeeid,
                CREATEDDATE = DateTime.Now,
                REQUESTDATA = requestdata,
                RESPONSEDATA = responsedata,
                IDPROFILE = idprofile
            };
            var logDao = new LogAgentDataNewDao();
            logDao.Create(data);
        }
        
        private string RequestBody(AgentDataNew agent)
        {
            var payeeid = int.Parse(agent.PayeeID, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);

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


            var dto = new AgentNewPayloadDTO
            {
                instanceId = taspenApi.FindByInstanceProfileName("JAKARTA"),
                code = agent.PayeeID,
                name = agent.Name,
                address = agent.Address,
                phone = agent.Phone != null && agent.Phone.Trim().Length > 0 ? agent.Phone : "-",
                email = agent.EmailAddress != null && agent.EmailAddress.Trim().Length > 0 ? agent.EmailAddress : "-",
                positionId = positionAgentDto.Id,
                licenseNumber = agent.LicenseID != null && agent.LicenseID.Trim().Length > 0 ? agent.LicenseID : "-",
                licenseDate = "-",
                licenseExpire_at = agent.ExpiryDate != null
                    ? agent.ExpiryDate.GetValueOrDefault().ToString("dd-MM-yyyy")
                    : "-",
                licenseStatus = "valid" //TODO
            };
            

            var payload = new AgentNewPayload {agentProfile = dto};

            var jsonString = JsonConvert.SerializeObject(payload);
            return jsonString;
        }
        
    }
}