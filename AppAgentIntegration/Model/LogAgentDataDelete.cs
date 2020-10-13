using System;

namespace AppAgentIntegration.Model
{
    public class LogAgentDataDelete
    {
        public int IDPROFILE { get; set; }
        public string REQUESTDATA { get; set; }
        public string RESPONSEDATA { get; set; }
        public DateTime? CREATEDDATE { get; set; }
    }
}