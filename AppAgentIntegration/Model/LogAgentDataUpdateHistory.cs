using System;

namespace AppAgentIntegration.Model
{
    public class LogAgentDataUpdateHistory
    {
        public int IDHISTORY { get; set; }
        public int IDAGENT { get; set; }
        public string REQUESTDATA { get; set; }
        public string RESPONSEDATA { get; set; }
        public DateTime? CREATEDDATE { get; set; }
    }
}