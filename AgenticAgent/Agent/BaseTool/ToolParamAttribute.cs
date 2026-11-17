[AttributeUsage(AttributeTargets.Parameter)]
public sealed class ToolParamAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}