using System.Text;
using System.Text.Json;
using AgenticAgent.Models.DTOs;
using AgenticAgent.Models.Messages;
using AgenticAgent.Models.Tools;

namespace AgenticAgent.OllamaChatClient;

// used ai for this
public interface IChatClient
{
    public Task<ChatResponse> SendChatAsync(
        List<Message> messages,
        CancellationToken cancellationToken = default);
}

public class CustomOllamaClient : IChatClient
{
    private readonly HttpClient _httpClient;
    private readonly string _llmModelName;
    private readonly List<ToolDefinition>? _toolDefinitions;


    public CustomOllamaClient(
        HttpClient _httpClient,
        string _llmModelName,
        List<ToolDefinition>? _toolDefinitions = null
    )
    {
           this._httpClient = _httpClient;
           this._llmModelName = _llmModelName;
           this._toolDefinitions = _toolDefinitions;
    }
    
    public async Task<ChatResponse> SendChatAsync(
        List<Message> messages,
        CancellationToken cancellationToken = default)
    {
        ChatRequest requestPayload =  new ChatRequest
        {
            Stream = false,
            Messages = messages,
            Tools = _toolDefinitions,
            Model =  _llmModelName,
        };
        var json = JsonSerializer.Serialize<ChatRequest>(requestPayload);
        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/chat")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        using var response = await _httpClient.SendAsync(request, cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var responseDto = JsonSerializer.Deserialize<ChatResponse>(responseJson);
        
        return responseDto!;
    }
}