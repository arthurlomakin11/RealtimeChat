namespace RealtimeChat.Persistence.Repositories;

public class MessageRepository(RealtimeChatDbContext dbContext, IMapper mapper) : IMessageRepository
{
    public async Task<MessageContent> GetByIdAsync(int id)
    {
        var entity = await dbContext.Messages
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (entity == null)
            throw new Exception("Message not found");

        return mapper.Map<MessageContent>(entity);
    }

    public async Task AddAsync(MessageContent message)
    {
        var entity = mapper.Map<MessageEntity>(message);
        await dbContext.Messages.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}