using RealtimeChat.Persistence.DB.Interfaces;

namespace RealtimeChat.Tests.Benchmarks;

public class DbSeeder
{
    public static async Task Seed(IRealtimeChatDbContext dbContext, int messageCount)
    {
        var generatedUser = DataGenerator.GenerateUser();
        var newUserEntry = dbContext.Users.Add(generatedUser);
        
        var generatedChatRoom = DataGenerator.GenerateChatRoom();
        var newChatRoomEntry = dbContext.ChatRooms.Add(generatedChatRoom);
        
        await dbContext.SaveChangesAsync();
        
        var userId = newUserEntry.Entity.Id;
        var chatRoomId = newChatRoomEntry.Entity.Id;
        
        var messages = DataGenerator.GenerateMessages(messageCount, userId, chatRoomId);
        
        await dbContext.Messages.AddRangeAsync(messages);
        await dbContext.SaveChangesAsync();
    }
}