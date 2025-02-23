namespace RealtimeChat.Persistence.Repositories;

public class ChatRoomRepository(RealtimeChatDbContext dbContext, IMapper mapper) : IChatRoomRepository
{
    public async Task<ChatRoom> GetByIdAsync(int id)
    {
        var entity = await dbContext.ChatRooms
            .Include(cr => cr.Messages)
            .Include(cr => cr.ChatRoomParticipants)
            .FirstOrDefaultAsync(cr => cr.Id == id);

        if (entity == null)
            throw new Exception("ChatRoom not found");

        return mapper.Map<ChatRoom>(entity);
    }

    public async Task AddAsync(ChatRoom chatRoom)
    {
        var entity = mapper.Map<ChatRoomEntity>(chatRoom);
        await dbContext.ChatRooms.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }
}