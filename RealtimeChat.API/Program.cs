using RealtimeChat.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddExploreEndpoints();
builder.AddCors();
builder.AddDbContext();
builder.AddGraphQlServer();
builder.AddAuth();
builder.AddCookies();
builder.AddLogging();
builder.AddMapping();
builder.AddDependencyInjectionServices();

var app = builder.Build();

app.UseExploreEndpointsForDevMode();
app.UseCors();
app.UseAuth();
app.UseGraphQlServer(args);

app.Run();