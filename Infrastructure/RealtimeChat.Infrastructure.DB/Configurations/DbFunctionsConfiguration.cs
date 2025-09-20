using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB.Utils;

namespace RealtimeChat.Infrastructure.DB.Configurations;

public class DbFunctionsConfiguration: ITypeConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.HasDbFunction(typeof(DatabaseFunctionsExtensions)
                .GetMethod(nameof(DatabaseFunctionsExtensions.JsonExtractPathText))!)
            .HasName("jsonb_extract_path_text")
            .IsBuiltIn();
    }
}