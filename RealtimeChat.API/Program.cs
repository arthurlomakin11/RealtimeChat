using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB;
using StrictId.HotChocolate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => 
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    })
);

builder.Services
    .AddGraphQLServer()
    .AddStrictId();

builder.Services.AddAuthorizationBuilder();

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<RealtimeChatDbContext>()
    .AddApiEndpoints();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<RealtimeChatDbContext>(optionsBuilder => 
    optionsBuilder.UseNpgsql(
        connectionString, 
        contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("RealtimeChat.Infrastructure.DB.Migrations")
    )
);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.Name = "RealtimeChat.Identity";
});

builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
    
app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.MapPost("/account/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Ok("Logged out");
});

app.MapGet("/auth-ping", () => "PING").RequireAuthorization();

app.MapGraphQL().RequireAuthorization();
app.RunWithGraphQLCommands(args);

app.Run();