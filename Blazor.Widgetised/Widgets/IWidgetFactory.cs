using System;

namespace Blazor.Widgetised
{
    public interface IWidgetFactory
    {
        void Register(string variantName, WidgetVariant variant);

        (Guid id, object mediator) Build(string variantName);

        (Guid id, object mediator) Build(WidgetDescription description);
    }
}