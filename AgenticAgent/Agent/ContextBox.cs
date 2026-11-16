using AgenticAgent.Models.Exceptions;
using AgenticAgent.Models.Messages;
using lab_one;


public class ContextBox
{
    public int Capacity { get; private set; }
    public List<Message> Messages { get; private set; }

    private ContextBox(int capacity)
    {
        Capacity = capacity;
        Messages = new List<Message>();
    }

    public static ContextBox MemoryTier1() => new ContextBox(10);
    public static ContextBox MemoryTierExperiment() => new ContextBox(5);
    public static ContextBox MemoryTier2() => new ContextBox(30);
    public static ContextBox MemoryTier3() => new ContextBox(40);
    
    
    public int getNumberOfMessages() => Messages.Count;
    
    
    public void AddAssistentMessage(string content, List<ToolCall>? toolCalls = null)
    {
        if (Capacity < Messages.Count)
        {
            throw new MessagesLimitReached("Cannot add more than " + Messages.Count + " messages.");
        }
        Messages.Add(
            new Message
            {
                Role = "tool",
                Content = content,
                ToolCalls = toolCalls
            });
    }
    public void AddToolMessage(string content, string toolName)
    {
        if (Capacity < Messages.Count)
        {
            throw new  MessagesLimitReached("Cannot add more than " + Messages.Count + " messages.");
        }
        Messages.Add(
            new Message
            {
                Role = "tool",
                Content = content,
                ToolName = toolName
            });
    }
    public void AddSystemMessage(string content)
    {
        if (Capacity < Messages.Count)
        {
            throw new  MessagesLimitReached("Cannot add more than " + Messages.Count + " messages.");
        }
        Messages.Add(
            new Message
            {
                Role = "system",
                Content = content,
            });
    }    
    public void AddUserMessage(string message)
    {
        if (Capacity < Messages.Count)
        {
            throw new  MessagesLimitReached("Cannot add more than " + Messages.Count + " messages.");
        }
        Messages.Add(
            new Message
            {
                Role = "user",
                Content = message,
            });
    }
    
}