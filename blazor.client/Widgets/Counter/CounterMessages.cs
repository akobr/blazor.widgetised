using Blazor.PureMvc.Messaging;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterMessage : IMessage
    {
        public class Increment : CounterMessage
        {
            // no member ( message type )
        }

        public class Decrement : CounterMessage
        {
            // no member ( message type )
        }

        public class Add : CounterMessage
        {
            public int Amount { get; set; }
        }
    }
}
