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

        void Deactivate(Guid widgetId);

        void Destroy(Guid widgetId);
    }
}
