using System;

namespace AppAgentIntegration.Model
{
    public class LogAgentDataUpdate
    {
        public int IDAGENT { get; set; }
        public string REQUESTDATA { get; set; }
        public string RESPONSEDATA { get; set; }
        public DateTime? CREATEDDATE { get; set; }
    }
}