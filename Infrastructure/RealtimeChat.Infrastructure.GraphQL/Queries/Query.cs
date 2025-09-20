using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RealtimeChat.Infrastructure.DB.Interface.Repositories;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Infrastructure.GraphQL;

public class Query(IMapper mapper)
{
    public async Task<ChatRoomGraph> GetChatRoom([Service] IChatRoomRepository chatRoomRepository, int roomId)
    {
        var room = await chatRoomRepository
            .GetAllAsync()
            .ProjectTo<ChatRoomGraph>(mapper.ConfigurationProvider)
            .FirstAsync(cr => cr.Id == roomId);
        
        return room;
    }
    
    public IQueryable<ChatRoomGraph> GetChatRooms([Service] IChatRoomRepository chatRoomRepository)
    {
        var query = chatRoomRepository
            .GetAllAsync()
            .ProjectTo<ChatRoomGraph>(mapper.ConfigurationProvider);
        
        return query;
    }
    
    public IQueryable<MessageGraph> GetMessages([Service] IMessageRepository messageRepository, int chatRoomId)
    {
        return messageRepository
            .GetAll()
            .ProjectTo<MessageGraph>(mapper.ConfigurationProvider)
            .Where(m => m.ChatRoomId == chatRoomId)
            .OrderBy(m => m.SentAt);
    }
    
    public IQueryable<MessageGraph> GetFilteredMessages([Service] IMessageRepository messageRepository, int chatRoomId,
        string searchString)
    {
        return messageRepository
            .GetFilteredByText(searchString)
            .ProjectTo<MessageGraph>(mapper.ConfigurationProvider)
            .Where(m => m.ChatRoomId == chatRoomId)
            .OrderBy(m => m.SentAt);
    }
}