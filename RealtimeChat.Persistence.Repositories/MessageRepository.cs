namespace RealtimeChat.Persistence.Repositories;

public class MessageRepository(IDbContextFactory<RealtimeChatDbContext> contextFactory, IMapper mapper) 
    : IMessageRepository
{
    public async Task<MessageContent> GetByIdAsync(int id)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var entity = await dbContext.Messages
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (entity == null)
            throw new Exception("Message not found");

        return mapper.Map<MessageContent>(entity);
    }

    public async Task AddAsync(MessageContent message)
    {
        await using var dbContext = await contextFactory.CreateDbContextAsync();
        var entity = mapper.Map<MessageEntity>(message);
        await dbContext.Messages.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}