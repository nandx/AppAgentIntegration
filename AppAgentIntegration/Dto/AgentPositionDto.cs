using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppAgentIntegration.Dto
{
    public class AgentPositionDto
    {
        [JsonProperty("meta")] public PositionMetaDto PositionMetaDto;

        [JsonProperty("agent/position")] public List<PositionAgentDto> ListAgentPosition { get; set; }
    }

    public class PositionAgentDto
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class PositionMetaDto
    {
        [JsonProperty("pagination")] public PositionMetaPaginationDto PositionMetaPaginationDto;
    }

    public class PositionMetaPaginationDto
    {
        [JsonProperty("total")] public int Total { get; set; }

        [JsonProperty("count")] public int Count { get; set; }

        [JsonProperty("per_page")] public int PerPage { get; set; }

        [JsonProperty("current_page")] public int CurrentPage { get; set; }

        [JsonProperty("total_pages")] public int TotalPages { get; set; }
    }
}