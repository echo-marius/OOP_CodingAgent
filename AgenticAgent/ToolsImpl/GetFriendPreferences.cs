namespace AgenticAgent.ToolsImpl;

public sealed class GetFriendPreferences : BaseTool
{
    private readonly Dictionary<string, string> _preferences = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Ion"]    = "Loves specialty coffee, mechanical keyboards, and sci-fi books.",
        ["Dragos"] = "Enjoys mountain biking, craft beer, and grilling.",
        ["Marius"] = "Interested in chess, minimalist watches, and jazz music.",
        ["Petru"]  = "Likes PC gaming, spicy food, and retro sneakers.",
        ["Pavel"]  = "Enjoys photography, hiking, and cooking Italian food."
    };

    [ToolMetadata(
        "get_friend_preferences",
        "Returns the hobbies, interests, and gift preferences of a specific friend. Use this when the user asks what a friend likes or needs gift ideas for them."
    )]
    public string Execute(
        [ToolParam("The friend's first name (e.g., \"Ion\", \"Dragos\")")] string friendName)
    {
        if (string.IsNullOrWhiteSpace(friendName))
        {
            return "ERROR: Please provide a valid friend name.";
        }

        return _preferences.TryGetValue(friendName.Trim(), out var prefs)
            ? $"{friendName}'s preferences: {prefs}"
            : $"No preferences found for '{friendName}'.";
    }
}