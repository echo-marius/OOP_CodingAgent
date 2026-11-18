using AgenticAgent;
using AgenticAgent.ConsoleClient;
using AgenticAgent.OllamaChatClient;
using AgenticAgent.Personality;
using AgenticAgent.ToolsImpl;
using lab_one;

Logger logger = new Logger(
    "AgenticAgent",
    new SimpleFormatter("[%t] [%l] %n: %m"),
    new ConsoleSink());
Logger.Level = LogLevel.Trace;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:11434"),
    Timeout = TimeSpan.FromMinutes(5)
};

logger.Log(LogLevel.Info, "Initializing Ollama client and tool box.");

var toolBox = new ToolBox()
    .Register(new GetListOfFriends())
    .Register(new GetCurrentDate())
    .Register(new GetFriendBirthday())
    .Register(new GetFriendPreferences())
    .Register(new GetShopCatalog());

var client = new CustomOllamaClient(
    _httpClient: httpClient,
    _llmModelName: "qwen2.5:7b-instruct",
    _toolDefinitions: toolBox.GetToolsDefinitions()
    );

var consoleClient = new ConsoleClient();
var personality = new  DonaldTrumpPersonality();

var agent = new Agent(
    consoleClient,
    client,
    personality,
    toolBox,
    logger,
    ContextBox.MemoryTierExperiment());



await agent.ChatAsync();

