using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;

using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Mapping;

namespace RealtimeChat.API;

public static class MappingExtensions
{
    public static void AddMapping(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(DomainToDbMappingProfile), typeof(DomainToGraphQlMappingProfile));
        
        var dbContextAssembly = typeof(RealtimeChatDbContext).Assembly;
        builder.Services.AddAutoMapper((serviceProvider, configuration) =>
        {
            configuration.AddCollectionMappers();
            configuration.AddExpressionMapping();
            configuration.UseEntityFrameworkCoreModel<RealtimeChatDbContext>(serviceProvider);
        }, dbContextAssembly);
    }
}