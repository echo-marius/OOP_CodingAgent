[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ToolMetadataAttribute(string name, string description) : Attribute
{
    public string Name { get; } = name;
    public string Description { get; } = description;
}