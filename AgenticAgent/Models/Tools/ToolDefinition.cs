using System.Text.Json.Serialization;

namespace AgenticAgent.Models.Tools;

public sealed class ToolDefinition
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "function";

    [JsonPropertyName("function")]
    public required FunctionDefinition Function { get; set; }
}