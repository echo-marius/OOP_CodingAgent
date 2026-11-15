using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using AgenticAgent;
using AgenticAgent.ConsoleClient;
using AgenticAgent.Models.DTOs;
using AgenticAgent.Models.Exceptions;
using AgenticAgent.Models.Messages;
using AgenticAgent.OllamaChatClient;
using AgenticAgent.Personality;
using lab_one;


public class Agent
{
    private readonly ContextBox _context;
    private readonly IConsoleClient _consoleClient;
    private readonly IChatClient _chatclient;
 
    private readonly ToolBox _toolBox;
    private readonly Personality _personality;
    private readonly Logger _logger;

    public Agent(
        IConsoleClient _consoleClient,
        IChatClient _chatclient,
        Personality _personality,
        ToolBox _toolBox,
        Logger _logger,
        ContextBox _context = null)
    {
        this._context = _context ?? ContextBox.MemoryTier1();
        this._personality = _personality ?? throw new ArgumentNullException(nameof(_personality));
        this._consoleClient = _consoleClient ?? throw new ArgumentNullException(nameof(_consoleClient));
        this._chatclient = _chatclient ?? throw new ArgumentNullException(nameof(_chatclient));
        this._toolBox = _toolBox ?? throw new ArgumentNullException(nameof(_toolBox));
        this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
        
        _context.AddSystemMessage(this._personality.InitialSystemPrompt);
    }
    public async Task ChatAsync()
    {
       
            _logger.Log(LogLevel.Info, "Agent started.");
            while (true)
            {
                try
                {
                    var input = _consoleClient.GetInput();

                    if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.Log(LogLevel.Warning, "User requested exit.");
                        break;
                    }

                    _context.AddUserMessage(input);
                    _logger.Log(LogLevel.Trace, "User requested received");
                    
                    ChatResponse response = await _chatclient.SendChatAsync(_context.Messages);
                    
                    _logger.Log(LogLevel.Trace, "Server response received");
                    
                    while (response.Message.ToolCalls is not null)
                    {

                        foreach (ToolCall toolCall in response.Message.ToolCalls)
                        {
                            _logger.Log(LogLevel.Trace, $"tool {toolCall.Function.Name} was called");
                            BaseTool? tool = this._toolBox.GetTool(toolCall.Function.Name);
                            string result = await tool.InvokeAsync(toolCall.Function.Arguments);
                            this._context.AddToolMessage(result, toolCall.Function.Name);
                        }

                        response = await _chatclient.SendChatAsync(_context.Messages);
                        _context.AddAssistentMessage(response.Message.Content,response.Message.ToolCalls);
                        _logger.Log(LogLevel.Trace, "Server response received");
                    }

                    double seconds = response.TotalDuration / 1_000_000_000.0;
                    this._logger.Log(LogLevel.Info, $"Number of messages exchanged {_context.getNumberOfMessages()} | Number of tokens send {response.NumberOfTokensIn} | Number of Tokens received {response.NumberOfTokensOut} | Time {seconds}");
                    _consoleClient.PrintAssistantMessage(response.Message.Content,_personality.Name);
                }
                catch (MessagesLimitReached exception)
                {
                    _logger.Log(LogLevel.Error, exception.Message);
                    int answer = this._consoleClient.MemoryLimitReachedInteraction(_context.Capacity,_context.getNumberOfMessages());
                    if (answer == 0)
                    {
                        _logger.Log(LogLevel.Trace, "User Quit");
                        break;
                    }
                    else
                    {
                        for(int i = 1;i <= answer;i++)
                            this._context.Messages.RemoveAt(i);
                    }
                }
                catch (Exception exception)
                {
                    _logger.Log(LogLevel.Error, exception.Message);
                }
            }

    }
}
