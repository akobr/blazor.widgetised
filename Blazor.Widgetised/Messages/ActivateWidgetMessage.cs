using System;

namespace Blazor.Widgetised.Messages
{
    public class ActivateWidgetMessage : IWidgetMessage
    {
        public ActivateWidgetMessage(Guid id, string containerKey)
        {
            WidgetId = id;
            ContainerKey = containerKey;
        }

        public Guid WidgetId { get; }

        public string ContainerKey { get; }

        public void ProcessMessage(IWidgetManagementService service)
        {
            service.Activate(WidgetId, ContainerKey);
        }
    }
}
