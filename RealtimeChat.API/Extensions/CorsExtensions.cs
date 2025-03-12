namespace RealtimeChat.API;

public static class CorsExtensions
{
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => 
            options.AddDefaultPolicy(corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins(builder.Configuration.GetValue<string>("AllowCorsAddress")!)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            })
        );
    }
}