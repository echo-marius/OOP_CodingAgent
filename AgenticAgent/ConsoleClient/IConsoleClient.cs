using System.Globalization;

namespace AgenticAgent.ConsoleClient;


public enum MesssageType
{
    ToolMessage,
    AssistantMessage
}

public interface IConsoleClient
{
    public string GetInput();
    public void PrintAssistantMessage(string message, string assistantName);
    public int MemoryLimitReachedInteraction(int limit, int inUse);
}

public class ConsoleClient : IConsoleClient
{
    public string GetInput()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("User > ");
        Console.ResetColor();
        return Console.ReadLine();
    }
    public void PrintAssistantMessage(string message, string assistantName)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{assistantName} > ");
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public int MemoryLimitReachedInteraction(int limit, int inUse)
    {
        Console.WriteLine($"{inUse} messages out of {limit} have been reached.");
        Console.WriteLine($"input 0 to leave, or the number of messages u want ot be deleted");
        Console.Write("input > ");
        int input;
        bool parsed = false;
        while (true)
        {
            parsed = Int32.TryParse(Console.ReadLine(), out input);
            if (!parsed ||  input < 0 || input > limit)
            {
                Console.WriteLine("insert a valid number");
                continue;
            }
            return input;
        }
    }
}


