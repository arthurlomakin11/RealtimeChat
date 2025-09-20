using Microsoft.EntityFrameworkCore;

namespace RealtimeChat.Infrastructure.DB.Utils;

public static class DatabaseFunctionsExtensions
{
    [DbFunction("jsonb_extract_path_text", IsBuiltIn = true)]
    public static string JsonExtractPathText(string json, string key)
    {
	    throw new NotSupportedException();
    }
}