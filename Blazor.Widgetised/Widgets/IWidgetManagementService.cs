using System;

namespace Blazor.Widgetised
{
    public interface IWidgetManagementService
    {
        (Guid id, object mediator) Build(string variantName);

        (Guid id, object mediator) Build(WidgetVariant variant);

        (Guid id, object mediator) Build(WidgetDescription description);

        (Guid id, object mediator) Start(string variantName, string containerKey);

        (Guid id, object mediator) Start(WidgetDescription description);

        void Activate(Guid widgetId, string containerKey);

        void Activate(IWidgetIdentifier identifier, string containerKey);

        void Deactivate(Guid widgetId);

        void Deactivate(IWidgetIdentifier identifier);

        void Destroy(Guid widgetId);

        void Destroy(IWidgetIdentifier identifier);
    }
}
