using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppAgentIntegration.Dto
{
    public class InstanceProfileResponseDto
    {
        [JsonProperty("instance/profile")] public List<InstanceProfileDto> ListInstanceProfile { get; set; }

        [JsonProperty("meta")] public InstanceProfileMetaDto InstanceProfileMetaDto { get; set; }
    }

    public class InstanceProfileDto
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("provinceId")] public string ProvinceId { get; set; }

        [JsonProperty("regionId")] public string RegionId { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("accNumber")] public string AccNumber { get; set; }

        [JsonProperty("picNik")] public string PicNik { get; set; }

        [JsonProperty("picNip")] public string PicNip { get; set; }

        [JsonProperty("picName")] public string PicName { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("parentId")] public int ParentId { get; set; }

        [JsonProperty("level")] public int Level { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("satkerId")] public string SatkerId { get; set; }

        [JsonProperty("vaNumber")] public string VaNumber { get; set; }
    }


    public class InstanceProfileMetaDto
    {
        [JsonProperty("pagination")] public InstanceProfilePaginationDto InstanceProfilePaginationDto { get; set; }
    }

    public class InstanceProfilePaginationDto
    {
        [JsonProperty("total")] public int Total { get; set; }

        [JsonProperty("count")] public int Count { get; set; }

        [JsonProperty("per_page")] public int PerPage { get; set; }

        [JsonProperty("current_page")] public int CurrentPage { get; set; }

        [JsonProperty("total_pages")] public int TotalPages { get; set; }
    }
}