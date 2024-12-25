using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB;
using StrictId.HotChocolate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .AddStrictId();
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication()
    .AddCookie();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<RealtimeChatDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<RealtimeChatDbContext>(optionsBuilder => 
    optionsBuilder.UseNpgsql(
        connectionString, 
        contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("RealtimeChat.Infrastructure.DB.Migrations")
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/account").MapIdentityApi<IdentityUser>();
app.MapGet("/ping", () => "PING");

app.MapGraphQL().RequireAuthorization();
app.RunWithGraphQLCommands(args);

app.Run();