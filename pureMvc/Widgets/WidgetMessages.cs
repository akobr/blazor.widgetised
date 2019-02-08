using Blazor.PureMvc.Messaging;

namespace Blazor.PureMvc.Widgets
{
    public abstract class WidgetMessage : ISystemMessage
    {
        public string WidgetKey { get; set; }

        public class Build : WidgetMessage
        {
            public string VariantName { get; set; }

            public WidgetVariant Variant { get; set; }
        }

        public class Activate : WidgetMessage
        {
            public string ContainerKey { get; set; }
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
