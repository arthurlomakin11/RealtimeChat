using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using RealtimeChat.Domain.Models;
using RealtimeChat.Persistence.DB.Entities;
using RealtimeChat.Persistence.DB.Interfaces;

namespace RealtimeChat.Persistence.Repositories;

public class ChatRoomRepository(IRealtimeChatDbContext dbContext, IMapper mapper) 
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