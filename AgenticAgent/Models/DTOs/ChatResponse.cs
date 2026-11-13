using System.Text.Json.Serialization;
using AgenticAgent.Models.Messages;

namespace AgenticAgent.Models.DTOs;

public sealed class ChatResponse
{
    [JsonPropertyName("model")] 
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("created_at")] 
    public DateTimeOffset CreatedAt { get; set; }
    [JsonPropertyName("message")] 
    public Message Message { get; set; } = new();

    [JsonPropertyName("done")] 
    public bool Done { get; set; }

    [JsonPropertyName("total_duration")] 
    public long TotalDuration { get; set; }

    [JsonPropertyName("prompt_eval_count")] 
    public int NumberOfTokensIn { get; set; }

    [JsonPropertyName("eval_count")] 
    public int NumberOfTokensOut { get; set; }
}