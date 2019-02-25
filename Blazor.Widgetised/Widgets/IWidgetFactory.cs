using System;

namespace Blazor.Widgetised
{
    public interface IWidgetFactory : IFactory<object, string>
    {
        void Register(string variantName, WidgetVariant variant);

        (Guid id, object mediator) Build(WidgetDescription description);
    }
}