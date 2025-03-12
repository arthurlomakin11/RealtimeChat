using RealtimeChat.API;
using RealtimeChat.Mapping;
using RealtimeChat.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddExploreEndpoints();
builder.AddCors();
builder.AddDbContext();
builder.AddGraphQlServer();
builder.AddAuth();
builder.AddCookies();

builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddAutoMapper(typeof(DomainToDbMappingProfile), typeof(DomainToGraphQLMappingProfile));

builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

app.UseExploreEndpointsForDevMode();
app.UseCors();
app.UseAuth();
app.UseGraphQlServer(args);

app.Run();