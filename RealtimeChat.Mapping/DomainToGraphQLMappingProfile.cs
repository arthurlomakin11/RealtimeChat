using AutoMapper;

using RealtimeChat.Domain.Models;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Mapping;

public class DomainToGraphQlMappingProfile : Profile
{
    public DomainToGraphQlMappingProfile()
    {
        // ChatRoom
        CreateMap<ChatRoom, ChatRoomGraph>();
        CreateMap<ChatRoomGraph, ChatRoom>();
        
        // Message
        CreateMap<Message, MessageGraph>();
        CreateMap<MessageGraph, Message>();
        
        // MessageContent
        CreateMap<MessageContent, IMessageContent>()
            .Include<TextMessageContent, TextMessageContentGraph>()
            .Include<ImageMessageContent, ImageMessageContentGraph>();
        
        CreateMap<TextMessageContent, TextMessageContentGraph>();
        CreateMap<ImageMessageContent, ImageMessageContentGraph>();
    }
}