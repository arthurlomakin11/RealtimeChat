using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Persistence.DB;

namespace RealtimeChat.API;

public static class AuthExtensions
{
    public static void AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<RealtimeChatDbContext>()
            .AddApiEndpoints();
    }
    
    public static void UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapGroup("/account").MapIdentityApi<ApplicationUser>();

        app.MapPost("/account/logout", async (HttpContext httpContext) =>
        {
            await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Results.Ok("Logged out");
        });

        app.MapGet("/auth-ping", () => "PING").RequireAuthorization();
    }
}