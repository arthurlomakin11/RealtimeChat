using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using RealtimeChat.Domain.Models;
using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Persistence.Repositories;

public class MessageRepository(RealtimeChatDbContext dbContext, IMapper mapper) 
    : IMessageRepository
{
    public IQueryable<Message> GetAll()
    {
        return dbContext.Messages
            .ProjectTo<Message>(mapper.ConfigurationProvider);
    }
    
    public IQueryable<Message> GetFilteredByText(string searchString)
    {
        const string contentName = nameof(MessageEntity.Content);

        return dbContext.Messages
            .Where(m =>
                DatabaseFunctionsExtensions.JsonExtractPathText(
                    EF.Property<string>(m, contentName), "Type") == "text" &&
                EF.Functions.ILike(
                    DatabaseFunctionsExtensions.JsonExtractPathText(
                        EF.Property<string>(m, contentName), "Text"),
                    $"%{searchString}%"))
            .ProjectTo<Message>(mapper.ConfigurationProvider);
    }

    public async Task<Message> AddAsync(Message message)
    {
        var entity = mapper.Map<MessageEntity>(message);
        var newEntity = await dbContext.Messages.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Message>(newEntity.Entity);
    }
    
    public async Task<Message> UpdateContentAsync(int messageId, MessageContent message)
    {
        var foundEntity = await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

        if (foundEntity is null) throw new Exception("Message not found");
        
        foundEntity.Content = mapper.Map<MessageContentEntity>(message);
        await dbContext.SaveChangesAsync();
        
        return mapper.Map<Message>(foundEntity);
    }
    
    public async Task<Message> DeleteAsync(int messageId)
    {
        var foundEntity = await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

        if (foundEntity is null) throw new Exception("Message not found");
        
        dbContext.Messages.Remove(foundEntity);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Message>(foundEntity);
    }
}