using System.Text.Json.Serialization;

namespace AgenticAgent.Models.Tools;

public sealed class ToolProperty
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }
}
