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

        public string Name { get; set; }

        public object Content { get; set; }
    }

    public class Message<TContent> : IMessage
    {
        public Message()
        {
            // no operation
        }

        public Message(string name)
        {
            Name = name;
        }

        public Message(string name, TContent content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }

        public TContent Content { get; set; }
    }
}
