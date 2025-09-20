using AutoMapper;
using HotChocolate.Subscriptions;
using RealtimeChat.Domain.Models;
using RealtimeChat.Infrastructure.DB.Interface.Repositories;
using RealtimeChat.Persistence.GraphQL;

namespace RealtimeChat.Infrastructure.GraphQL;

public class MessageMutation(ITopicEventSender eventSender, IMapperBase mapper)
{
    public async Task<MessageGraph> SendMessage([Service] IMessageRepository messageRepository, 
        int chatRoomId, string senderId, string text)
    {
        var message = await messageRepository.AddAsync(new Message
        {
            SenderId = senderId,
            ChatRoomId = chatRoomId,
            Content = new TextMessageContent(text)
        });
        var messageGraph = mapper.Map<MessageGraph>(message);

        await eventSender.SendAsync("MessageUpdated", new MessageUpdatedEvent
        {
            EventType = "ADDED",
            Message = messageGraph
        });

        return messageGraph;
    }

    public async Task<MessageGraph> EditMessage([Service] IMessageRepository messageRepository, 
        int messageId, string newText)
    {
        var message = await messageRepository.UpdateContentAsync(messageId, new TextMessageContent(newText));
        var messageGraph = mapper.Map<MessageGraph>(message);
        
        await eventSender.SendAsync("MessageUpdated", new MessageUpdatedEvent
        {
            EventType = "UPDATED",
            Message = messageGraph
        });

        return messageGraph;
    }

    public async Task<MessageGraph> DeleteMessage([Service] IMessageRepository messageRepository, int messageId)
    {
        var message = await messageRepository.DeleteAsync(messageId);
        var messageGraph = mapper.Map<MessageGraph>(message);
        
        await eventSender.SendAsync("MessageUpdated", new MessageUpdatedEvent
        {
            EventType = "DELETED",
            Message = messageGraph
        });

        return messageGraph;
    }
}