using System;

namespace AppAgentIntegration.Model
{
    public class LogAgentDataNew
    {
        public int IDAGENT { get; set; }
        public String REQUESTDATA { get; set; }
        public String RESPONSEDATA { get; set; }
        public DateTime? CREATEDDATE { get; set; }
        public int? IDPROFILE { get; set; }
    }
}