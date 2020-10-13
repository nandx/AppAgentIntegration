using Newtonsoft.Json;

namespace AppAgentIntegration.Payload
{
    public class AgentNewPayload
    {
        [JsonProperty("agent/profile")] public AgentNewPayloadDTO agentProfile { get; set; }

    }
    
    public class AgentNewPayloadDTO
    {
        [JsonProperty("instanceId")] public int? instanceId { get; set; }

        [JsonProperty("code")] public string code { get; set; }

        [JsonProperty("name")] public string name { get; set; }

        [JsonProperty("address")] public string address { get; set; }

        [JsonProperty("phone")] public string phone { get; set; }

        [JsonProperty("email")] public string email { get; set; }

        [JsonProperty("positionId")] public int? positionId { get; set; }

        [JsonProperty("licenseNumber")] public string licenseNumber { get; set; }

        [JsonProperty("licenseDate")] public string licenseDate { get; set; }

        [JsonProperty("licenseExpire_at")] public string licenseExpire_at { get; set; }

        [JsonProperty("licenseStatus")] public string licenseStatus { get; set; }
    }
}