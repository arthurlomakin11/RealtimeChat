using AutoMapper;
using RealtimeChat.Persistence.GraphQL;
using RealtimeChat.Persistence.Repositories;

namespace RealtimeChat.Infrastructure.GraphQL;

public class Query(IChatRoomRepository chatRoomRepository, IMapper mapper)
{
    public async Task<ChatRoomGraph> GetChatRoomAsync(int id)
    {
        var chatRoomDomain = await chatRoomRepository.GetByIdAsync(id);
        return mapper.Map<ChatRoomGraph>(chatRoomDomain);
    }
}