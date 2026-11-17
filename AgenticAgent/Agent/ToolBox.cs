using AgenticAgent.Models.Tools;

public class ToolBox
{
    private List<BaseTool>? _tools = new List<BaseTool>();

    public ToolBox Register(BaseTool tool)
    {
        _tools.Add(tool);
        return this;
    }

    public List<ToolDefinition>? GetToolsDefinitions()
    {
        return _tools
            .Select(t => t.GetSchema())
            .ToList();
    }

    public BaseTool? GetTool(string toolName)
    {
        return _tools.FirstOrDefault(t => t.Name.Equals(toolName));
    }
}
