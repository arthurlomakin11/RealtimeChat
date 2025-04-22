using Microsoft.EntityFrameworkCore;

namespace RealtimeChat.Infrastructure.DB;

public static class Extensions
{
    public static ModelBuilder ApplyConfiguration(this ModelBuilder modelBuilder, ITypeConfiguration typeConfiguration)
    {
        typeConfiguration.Configure(modelBuilder);
        return modelBuilder;
    }
}