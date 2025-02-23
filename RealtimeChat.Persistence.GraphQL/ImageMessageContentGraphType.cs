namespace RealtimeChat.Persistence.GraphQL;

public class ImageMessageContentGraphType : ObjectType<ImageMessageContentGraph>
{
    protected override void Configure(IObjectTypeDescriptor<ImageMessageContentGraph> descriptor)
    {
        descriptor.Name("ImageMessageContent");
        descriptor.Field(x => x.Url).Type<NonNullType<StringType>>();
        descriptor.Field(x => x.Caption).Type<StringType>(); // Nullable
    }
}