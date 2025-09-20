using RealtimeChat.API;
using RealtimeChat.Infrastructure.DB.Context;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var config = builder.Configuration;

builder.AddExploreEndpoints();
builder.AddCors();
builder.Services.AddDbContext(config, env);
builder.Services
    .AddGraphQlServer<RealtimeChatDbContext>(env);
builder.AddAuth();
builder.AddCookies();
builder.AddLogging();
builder.AddMapping();

var app = builder.Build();

app.UseExploreEndpointsForDevMode();
app.UseCors();
app.UseAuth();
app.UseGraphQlServer(args);

app.Run();