using AutoMapper.QueryableExtensions;

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