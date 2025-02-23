using AutoMapper;
using RealtimeChat.Domain;
using RealtimeChat.Persistence.DB;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Mapping;

public class DomainToGraphQLMappingProfile : Profile
{
    public DomainToGraphQLMappingProfile()
    {
        CreateMap<ChatRoom, ChatRoomGraph>();
        CreateMap<MessageEntity, MessageGraph>()
            .ForMember(dest => dest.Content, 
                opt => 
                    opt.MapFrom(src => src.ContentJson));

        CreateMap<TextMessageContent, TextMessageContentGraph>();
        CreateMap<ImageMessageContent, ImageMessageContentGraph>();
    }
}