namespace AgenticAgent.ToolsImpl;

public sealed class GetCurrentDate : BaseTool
{
    [ToolMetadata(
        "get_current_date",
        "Returns the current local date and day of the week (format: yyyy-MM-dd, DayOfWeek). Use this when the user asks for today's date or needs to calculate upcoming birthdays."
    )]
    public string Execute()
    {
        var now = DateTime.Now;
        return $"{now:yyyy-MM-dd} ({now.DayOfWeek})";
    }
}