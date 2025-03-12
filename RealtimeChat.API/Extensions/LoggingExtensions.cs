namespace RealtimeChat.API;

public static class LoggingExtensions
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.SetMinimumLevel(builder.Environment.IsDevelopment() 
            ? LogLevel.Critical 
            : LogLevel.Information);
    }
}