namespace RealtimeChat.Persistence.Repositories;

public class MessageRepository(IDbContextFactory<RealtimeChatDbContext> contextFactory, IMapper mapper) 
    : IMessageRepository
{
    public async Task<Message> GetByIdAsync(int id)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var entity = await dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        
        if (entity == null) throw new Exception("Message not found");

        return mapper.Map<Message>(entity);
    }

    public async Task<Message> AddAsync(Message message)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var entity = mapper.Map<MessageEntity>(message);
        var newEntity = await dbContext.Messages.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Message>(newEntity.Entity);
    }
    
    public async Task<Message> UpdateContentAsync(int messageId, MessageContent message)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var foundEntity = await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

        if (foundEntity is null) throw new Exception("Message not found");
        
        foundEntity.ContentJson = mapper.Map<string>(message);
        await dbContext.SaveChangesAsync();
        
        return mapper.Map<Message>(foundEntity);
    }
    
    public async Task<Message> DeleteAsync(int messageId)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var foundEntity = await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

        if (foundEntity is null) throw new Exception("Message not found");
        
        dbContext.Messages.Remove(foundEntity);
        await dbContext.SaveChangesAsync();

        return mapper.Map<Message>(foundEntity);
    }
}