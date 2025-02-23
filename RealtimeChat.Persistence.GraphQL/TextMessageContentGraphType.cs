namespace RealtimeChat.Persistence.GraphQL;

public class TextMessageContentGraphType : ObjectType<TextMessageContentGraph>
{
    protected override void Configure(IObjectTypeDescriptor<TextMessageContentGraph> descriptor)
    {
        descriptor.Name("TextMessageContent");
        descriptor.Field(x => x.Text).Type<NonNullType<StringType>>();
    }
}