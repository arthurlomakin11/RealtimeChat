namespace RealtimeChat.Persistence.Repositories;

public interface IMessageRepository
{
    IQueryable<Message> GetAll();
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateContentAsync(int messageId, MessageContent message);
    Task<Message> DeleteAsync(int messageId);
}