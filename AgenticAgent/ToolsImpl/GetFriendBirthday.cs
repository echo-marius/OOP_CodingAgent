namespace AgenticAgent.ToolsImpl;

public sealed class GetFriendBirthday : BaseTool
{
    private readonly Dictionary<string, string> _birthdays = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Ion"]    = "1998-03-14",
        ["Dragos"] = "1997-07-22",
        ["Marius"] = "1999-11-15",
        ["Petru"]  = "1998-01-09",
        ["Pavel"]  = "2000-09-30"
    };

    [ToolMetadata(
        "get_friend_birthday",
        "Returns the birthday of a specific friend. Use this when the user asks when a friend's birthday is or wants to check upcoming birthdays."
    )]
    public string Execute(
        [ToolParam("The friend's first name (e.g., \"Ion\", \"Dragos\")")] string friendName)
    {
        if (string.IsNullOrWhiteSpace(friendName))
        {
            return "ERROR: Please provide a valid friend name.";
        }

        return _birthdays.TryGetValue(friendName.Trim(), out var birthday)
            ? $"{friendName}'s birthday is {birthday}."
            : $"No birthday found for '{friendName}'.";
    }
}
