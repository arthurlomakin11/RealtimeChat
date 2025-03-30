using AutoMapper;

using RealtimeChat.Domain;
using RealtimeChat.Persistence.DB;

namespace RealtimeChat.Mapping;

public class DomainToDbMappingProfile : Profile
{
    public DomainToDbMappingProfile()
    {
        // ChatRoom
        CreateMap<ChatRoom, ChatRoomEntity>();
        CreateMap<ChatRoomEntity, ChatRoom>();
        
        // Message
        CreateMap<Message, MessageEntity>()
            .ForMember(dest => dest.UserId,
                opt => 
                    opt.MapFrom(src => src.SenderId));
        
        CreateMap<MessageEntity, Message>()
            .ForMember(dest => dest.SenderId, 
                opt => 
                    opt.MapFrom(src => src.UserId));
        
        // MessageContentEntity -> MessageContent
        CreateMap<MessageContentEntity, MessageContent>()
            .Include<TextMessageContentEntity, TextMessageContent>()
            .Include<ImageMessageContentEntity, ImageMessageContent>();
        
        CreateMap<TextMessageContentEntity, TextMessageContent>();
        CreateMap<ImageMessageContentEntity, ImageMessageContent>();
        
        // MessageContent -> MessageContentEntity
        CreateMap<MessageContent, MessageContentEntity>()
            .Include<TextMessageContent, TextMessageContentEntity>()
            .Include<ImageMessageContent, ImageMessageContentEntity>();

        CreateMap<TextMessageContent, TextMessageContentEntity>();
        CreateMap<ImageMessageContent, ImageMessageContentEntity>();
        
        // ChatRoomParticipant
        CreateMap<ChatRoomParticipant, ChatRoomParticipantEntity>();
        CreateMap<ChatRoomParticipantEntity, ChatRoomParticipant>();
    }
}