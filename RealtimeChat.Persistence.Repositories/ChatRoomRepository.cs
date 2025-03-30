using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using RealtimeChat.Domain;
using RealtimeChat.Infrastructure.DB;
using RealtimeChat.Persistence.DB;

namespace RealtimeChat.Persistence.Repositories;

public class ChatRoomRepository(RealtimeChatDbContext dbContext, IMapper mapper) 
    : IChatRoomRepository
{
    public IQueryable<ChatRoom> GetAllAsync()
    {
        return dbContext.ChatRooms
            .Include(cr => cr.Messages)
            .Include(cr => cr.ChatRoomParticipants)
            .ProjectTo<ChatRoom>(mapper.ConfigurationProvider);
    }

    public async Task AddAsync(ChatRoom chatRoom)
    {
        var entity = mapper.Map<ChatRoomEntity>(chatRoom);
        await dbContext.ChatRooms.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}