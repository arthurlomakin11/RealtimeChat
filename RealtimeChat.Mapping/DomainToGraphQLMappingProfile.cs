using AutoMapper;
using RealtimeChat.Domain;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Mapping;

public class DomainToGraphQlMappingProfile : Profile
{
    public DomainToGraphQlMappingProfile()
    {
        // ChatRoom
        CreateMap<ChatRoom, ChatRoomGraph>()
            .ForMember(dest => dest.Messages, 
                opt => 
                    opt.MapFrom(src => src.Messages));
        
        CreateMap<ChatRoomGraph, ChatRoom>()
            .ForMember(dest => dest.Messages, 
                opt => 
                    opt.MapFrom(src => src.Messages));
        
        // Message
        CreateMap<Message, MessageGraph>()
            .ForMember(dest => dest.Content, 
                opt => 
                    opt.MapFrom(src => src.Content));
        
        CreateMap<MessageGraph, Message>()
            .ForMember(dest => dest.Content, 
                opt => 
                    opt.MapFrom(src => src.Content));
        
        // MessageContent
        CreateMap<MessageContent, IMessageContent>()
            .Include<TextMessageContent, TextMessageContentGraph>()
            .Include<ImageMessageContent, ImageMessageContentGraph>();
        
        CreateMap<TextMessageContent, TextMessageContentGraph>();
        CreateMap<ImageMessageContent, ImageMessageContentGraph>();
    }
}