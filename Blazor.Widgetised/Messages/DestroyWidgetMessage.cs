using System;

namespace Blazor.Widgetised.Messages
{
    public class DestroyWidgetMessage : IWidgetMessage
    {
        public DestroyWidgetMessage(Guid id)
        {
            WidgetId = id;
        }

        public Guid WidgetId { get; }

        public void ProcessMessage(IWidgetManagementService service)
        {
            service.Destroy(WidgetId);
        }
    }
}
