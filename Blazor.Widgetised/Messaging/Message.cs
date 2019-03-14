namespace Blazor.Widgetised.Messaging
{
    public class Message : IMessage
    {
        public Message()
        {
            // no operation
        }

        public Message(string name)
        {
            Name = name;
        }

        public Message(string name, object content)
        {
            Name = name;
            Content = content;
        }

        public string? Name { get; set; }

        public object? Content { get; set; }
    }

    public class Message<TContent> : IMessage
    {
#pragma warning disable CS8618
        // CS8618: Non-nullable field is uninitialized.
        // Reason: The property Content can't be set as nullable because of error
        //         CS8627: A nullable type parameter must be known to be a value type or non-nullable reference type.

        public Message()
        {
            // no operation
        }

        public Message(string name)
        {
            Name = name;
        }

#pragma warning restore CS8618

        public Message(string name, TContent content)
        {
            Name = name;
            Content = content;
        }

        public string? Name { get; set; }

        public TContent Content { get; set; }
    }
}
