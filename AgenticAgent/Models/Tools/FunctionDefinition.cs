using System.Text.Json.Serialization;

namespace AgenticAgent.Models.Tools;

public sealed class FunctionDefinition
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("parameters")]
    public required ToolParameters Parameters { get; set; }
}