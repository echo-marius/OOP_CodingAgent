namespace AgenticAgent.Personality;

public abstract class Personality
{
    public string Name { get; }
    public string InitialSystemPrompt { get; }

    protected Personality(string name, string initialSystemPrompt)
    {
        Name = name;
        InitialSystemPrompt = initialSystemPrompt;
    }

    public override string ToString() => Name;
}