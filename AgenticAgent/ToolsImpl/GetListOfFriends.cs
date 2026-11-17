namespace AgenticAgent.ToolsImpl;

public sealed class GetListOfFriends : BaseTool
{
    private readonly List<string> _friends = ["Ion", "Dragos", "Marius", "Petru", "Pavel"];

    [ToolMetadata(
        "get_list_of_friends",
        "Returns the list of all friends' names. Use this when the user asks who their friends are or needs to check all friends before looking up details."
    )]
    public string Execute()
    {
        return string.Join(", ", _friends);
    }
}