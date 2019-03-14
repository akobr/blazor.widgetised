using System;

namespace Blazor.Widgetised.Messages
{
    class DeactivateWidgetMessage : IWidgetMessage
    {
        public DeactivateWidgetMessage(Guid id)
        {
            WidgetId = id;
        }

        public Guid WidgetId { get; }

        public void ProcessMessage(IWidgetManagementService service)
        {
            service.Deactivate(WidgetId);
        }
    }
}
