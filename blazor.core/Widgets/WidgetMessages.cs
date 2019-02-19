using Blazor.Core.Messaging;

namespace Blazor.Core.Widgets
{
    public abstract class WidgetMessage : ISystemMessage
    {
        public string VariantName { get; set; }

        public string Position { get; set; }

        public class Build : WidgetMessage
        {
            public WidgetVariant Variant { get; set; }
        }

        public class Activate : WidgetMessage
        {
            // no member ( message type )
        }

        public class Deactivate : WidgetMessage
        {
            // no member ( message type )
        }

        public class Destroy : WidgetMessage
        {
            // no member ( message type )
        }
    }
}
