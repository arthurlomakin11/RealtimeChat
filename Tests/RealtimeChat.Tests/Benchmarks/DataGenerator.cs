using Bogus;
using RealtimeChat.Persistence.DB.Entities;

namespace RealtimeChat.Tests.Benchmarks;

public static class DataGenerator
{
    public static ApplicationUser GenerateUser()
    {
        var faker = new Faker();

        var user = new ApplicationUser
        {
            UserName = faker.Person.UserName,
            NormalizedUserName = null,
            Email = null,
            NormalizedEmail = null,
            EmailConfirmed = false,
            PasswordHash = null,
            SecurityStamp = null,
            ConcurrencyStamp = null,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            Messages = [],
            ChannelParticipants = []
        };

        return user;
    }
    
    public static ChatRoomEntity GenerateChatRoom()
    {
        var faker = new Faker();

        var chatRoom = new ChatRoomEntity
        {
            Name = faker.Company.CompanyName()
        };

        return chatRoom;
    }
    
    public static List<MessageEntity> GenerateMessages(int count, string userId, int chatRoomId)
    {
        var messages = new List<MessageEntity>();

        for (var i = 0; i < count; i++)
        {
            var message = new MessageEntity
            {
                UserId = userId,
                ChatRoomId = chatRoomId,
                Content = GenerateRandomMessageContent()
            };

            messages.Add(message);
        }

        return messages;
    }

    private static MessageContentEntity GenerateRandomMessageContent()
    {
        var faker = new Faker();
        
        if (faker.Random.Bool())
        {
            return new TextMessageContentEntity(faker.Lorem.Sentence());
        }

        return new ImageMessageContentEntity(faker.Image.PicsumUrl(), faker.Lorem.Sentence());
    }
}