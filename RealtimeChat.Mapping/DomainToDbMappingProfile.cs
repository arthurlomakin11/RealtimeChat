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
                opt => 
                    opt.MapFrom(src => src.Messages))
            .ForMember(dest => dest.ChatRoomParticipants, 
                opt => 
                    opt.MapFrom(src => src.Participants));
        
        CreateMap<ChatRoomEntity, ChatRoom>()
            .ConstructUsing((src, context) => new ChatRoom(src.Id, src.Name)
                .AddMessages(src.Messages.Select(m => context.Mapper.Map<Message>(m))));
        
        // Message
        CreateMap<Message, MessageEntity>()
            .ForMember(dest => dest.ContentJson,
                opt => 
                    opt.MapFrom(src => 
                        JsonConvert.SerializeObject(src, 
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            })));

        CreateMap<MessageEntity, Message>()
            .ForMember(dest => dest.SenderId, 
                opt => 
                    opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Content, 
                opt => 
                    opt.MapFrom(src => 
                        JsonConvert.DeserializeObject<MessageContent>(src.ContentJson,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto,
                                Converters = new List<JsonConverter>
                                {
                                    JsonConverters.GetJsonSubTypesConverter()
                                }
                            })));

        // ChatRoomParticipant
        CreateMap<ChatRoomParticipant, ChatRoomParticipantEntity>()
            .ForMember(dest => dest.Id, 
                opt => 
                    opt.MapFrom(src => src.Id));

        CreateMap<ChatRoomParticipantEntity, ChatRoomParticipant>()
            .ConstructUsing(src => new ChatRoomParticipant(src.Id, src.ChatRoomId, src.UserId));
    }
}