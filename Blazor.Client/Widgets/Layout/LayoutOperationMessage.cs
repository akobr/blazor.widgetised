using Blazor.Core.Messaging;

namespace Blazor.Client.Widgets.Layout
{
    public class LayoutOperationMessage : IMessage
    {
        public string TargetContainer { get; set; }

        public string TargetWidgetVariant { get; set; }
    }
}
