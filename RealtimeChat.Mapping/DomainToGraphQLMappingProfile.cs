using AutoMapper;
using RealtimeChat.Domain;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Mapping;

public class DomainToGraphQLMappingProfile : Profile
{
    public DomainToGraphQLMappingProfile()
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
                opt => opt.Ignore());
        
        CreateMap<MessageGraph, Message>()
            .ForMember(dest => dest.Content, 
                opt => opt.Ignore());
        
        // MessageContent
        CreateMap<string, MessageContent>()
            .ConvertUsing<MessageContentConverter>();
        
        CreateMap<TextMessageContent, TextMessageContentGraph>();
        CreateMap<ImageMessageContent, ImageMessageContentGraph>();
        
        CreateMap<TextMessageContentGraph, TextMessageContent>();
        CreateMap<ImageMessageContentGraph, ImageMessageContent>();
    }
}