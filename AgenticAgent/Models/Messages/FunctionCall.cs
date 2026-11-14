using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgenticAgent.Models.Messages;

public sealed class FunctionCall
{
    [JsonPropertyName("index")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Index { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("arguments")]
    public JsonElement Arguments { get; set; }
}