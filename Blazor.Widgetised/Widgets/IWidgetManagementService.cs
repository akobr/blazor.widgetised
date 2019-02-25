using System;

namespace Blazor.Widgetised
{
    public interface IWidgetManagementService
    {
        WidgetInfo Build(string variantName);

        WidgetInfo Build(WidgetVariant variant);

        WidgetInfo Build(WidgetDescription description);

        WidgetInfo Start(string variantName, string containerKey);

        WidgetInfo Start(WidgetDescription description);

        void Activate(Guid widgetId, string containerKey);

        void Activate(IWidgetIdentifier identifier, string containerKey);

        void Deactivate(Guid widgetId);

        void Deactivate(IWidgetIdentifier identifier);

        void Destroy(Guid widgetId);

        void Destroy(IWidgetIdentifier identifier);
    }
}
