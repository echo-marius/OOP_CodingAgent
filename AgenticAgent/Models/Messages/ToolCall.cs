using System.Text.Json.Serialization;

namespace AgenticAgent.Models.Messages;

public sealed class ToolCall
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "function";

    [JsonPropertyName("function")]
    public FunctionCall Function { get; set; } = new();
}