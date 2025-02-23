namespace RealtimeChat.Persistence.GraphQL;

public class MessageContentUnionType : UnionType
{
    protected override void Configure(IUnionTypeDescriptor descriptor)
    {
        descriptor.Name("MessageContent");
        descriptor.Type<TextMessageContentGraphType>();
        descriptor.Type<ImageMessageContentGraphType>();
    }
}