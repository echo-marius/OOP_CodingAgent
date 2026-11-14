namespace AgenticAgent.Models.Exceptions;

public class MessagesLimitReached : Exception
{
    public MessagesLimitReached(string message) : base(message)
    {
        
    }

    public MessagesLimitReached(string message, Exception innerException) : base(message, innerException)
    {
        
    }
    
}