using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Infrastructure.DB.Configurations;

public class IdentityConfiguration: ITypeConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("users");
        builder.Entity<IdentityRole>().ToTable("roles");
        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
    }
}