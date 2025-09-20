using Microsoft.EntityFrameworkCore;

namespace RealtimeChat.Infrastructure.DB;

public interface ITypeConfiguration
{
    void Configure(ModelBuilder builder);
}