using RealtimeChat.Domain.Models;

namespace RealtimeChat.Persistence.Repositories;

public interface IMessageRepository
{
    IQueryable<Message> GetAll();
    IQueryable<Message> GetFilteredByText(string searchString);
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateContentAsync(int messageId, MessageContent message);
    Task<Message> DeleteAsync(int messageId);
}