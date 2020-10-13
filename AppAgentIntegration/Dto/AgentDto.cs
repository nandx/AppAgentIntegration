using Newtonsoft.Json;

namespace AppAgentIntegration.Dto
{
    public class AgentDto
    {
        [JsonProperty("agent/profile")] public AgentProfileDto listAgentProfile { get; set; }

    }
    
    public class AgentProfileDto
    {
        [JsonProperty("id")] public int id { get; set; }


        [JsonProperty("code")] public string code { get; set; }


        [JsonProperty("name")] public string name { get; set; }

        [JsonProperty("address")] public string address { get; set; }


        [JsonProperty("email")] public string email { get; set; }


        [JsonProperty("licenseNumber")] public string licenseNumber { get; set; }

        [JsonProperty("licenseDate")] public string licenseDate { get; set; }

        [JsonProperty("licenseExpireAt")] public string licenseExpireAt { get; set; }

        [JsonProperty("licenseStatus")] public string licenseStatus { get; set; }

        [JsonProperty("instance")] public InstanceDto instance { get; set; }

        [JsonProperty("position")] public PositionDto position { get; set; }
    }

    public class InstanceDto
    {
        [JsonProperty("id")] public int id { get; set; }


        [JsonProperty("name")] public string name { get; set; }


        [JsonProperty("address")] public string address { get; set; }

        [JsonProperty("regionId")] public int? regionId { get; set; }

        [JsonProperty("phone")] public string phone { get; set; }

        [JsonProperty("accNumber")] public string accNumber { get; set; }

        [JsonProperty("picNik")] public string picNik { get; set; }

        [JsonProperty("picNip")] public string picNip { get; set; }

        [JsonProperty("picName")] public string picName { get; set; }

        [JsonProperty("email")] public string email { get; set; }


        [JsonProperty("parentId")] public int? parentId { get; set; }

        [JsonProperty("level")] public int? level { get; set; }

        [JsonProperty("satkerId")] public string satkerId { get; set; }

        [JsonProperty("vaNumber")] public string vaNumber { get; set; }
    }


    public class PositionDto
    {
        [JsonProperty("id")] public int id { get; set; }


        [JsonProperty("name")] public string name { get; set; }
    }
}