using AutoMapper;
using Newtonsoft.Json;
using RealtimeChat.Domain;
using RealtimeChat.Persistence.DB;

namespace RealtimeChat.Mapping;

public class DomainToDbMappingProfile : Profile
{
    public DomainToDbMappingProfile()
    {
        // ChatRoom
        CreateMap<ChatRoom, ChatRoomEntity>()
            .ForMember(dest => dest.Messages, 
                opt => opt.Ignore())
            .ForMember(dest => dest.ChatRoomParticipants, 
                opt => opt.Ignore());

        CreateMap<ChatRoomEntity, ChatRoom>()
            .ConstructUsing(src => new ChatRoom(src.Id, src.Name));

        // Message
        CreateMap<MessageContent, MessageEntity>()
            .ForMember(dest => dest.ContentJson,
                opt => 
                    opt.MapFrom(src => 
                        JsonConvert.SerializeObject(src, 
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            })));

        CreateMap<MessageEntity, MessageContent>()
            .ConstructUsing(src => JsonConvert.DeserializeObject<MessageContent>(src.ContentJson, 
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                })!);

        // ChatRoomParticipant
        CreateMap<ChatRoomParticipant, ChatRoomParticipantEntity>()
            .ForMember(dest => dest.ChatRoomParticipantId, 
                opt => 
                    opt.MapFrom(src => src.Id));

        CreateMap<ChatRoomParticipantEntity, ChatRoomParticipant>()
            .ConstructUsing(src => 
                new ChatRoomParticipant(src.ChatRoomParticipantId, src.ChatRoomId, src.UserId));
    }
}