using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RealtimeChat.Infrastructure.DB.Context;
using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.API;

public static class AuthExtensions
{
    public static void AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ExternalAuthService>();
        
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                googleOptions.CallbackPath = "/signin-google";
                
                googleOptions.Events.OnCreatingTicket = async context =>
                {
                    var authService = 
                        context.HttpContext.RequestServices.GetRequiredService<ExternalAuthService>();
                    
                    await authService.HandleExternalLoginAsync(context.Principal!, "Google");
                };
            });
        
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

        app.MapGet("/auth/external-login/{provider}", (string provider, HttpContext _, string? returnUrl) =>
        {
            var redirectUri = $"/auth/external-callback?returnUrl={Uri.EscapeDataString(returnUrl ?? "/")}";
            var props = new AuthenticationProperties
            {
                RedirectUri = redirectUri,
                Items =
                {
                    ["scheme"] = provider
                }
            };

            return Results.Challenge(props, [provider]);
        });
        
        app.MapGet("/auth/external-callback", async (HttpContext context) =>
        {
            var result = await context.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (!result.Succeeded || result.Principal == null)
            {
                return Results.Unauthorized();
            }

            var authService = context.RequestServices.GetRequiredService<ExternalAuthService>();
            var user = await authService.HandleExternalLoginAsync(result.Principal,
                result.Properties?.Items["scheme"] ?? "Google");
            
            await context.SignOutAsync(IdentityConstants.ExternalScheme);

            return user == null 
                ? Results.BadRequest("Failed to log in")
                : Results.Redirect("/");
        });
    }
}