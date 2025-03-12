namespace RealtimeChat.API;

public static class DevModeExtensions
{
    public static void AddExploreEndpoints(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    
    public static void UseExploreEndpointsForDevMode(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}