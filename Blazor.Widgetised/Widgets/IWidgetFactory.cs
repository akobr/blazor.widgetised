using System;

namespace Blazor.Widgetised
{
    public interface IWidgetFactory : IFactory<WidgetInfo, string>
    {
        void Register(string variantName, WidgetVariant variant);

        WidgetInfo Build(WidgetDescription description);
    }
}