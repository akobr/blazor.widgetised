using System;

namespace Blazor.Widgetised
{
    public interface IWidgetManagementService
    {
        void Build(string variantKey);

        void Build(WidgetVariant variant);

        void Build(WidgetDescription description);

        void Start(string variantKey, string containerKey);

        void Start(WidgetDescription description);

        void Activate(Guid widgetKey, string containerKey);

        void Activate(IWidgetIdentifier identifier, string containerKey);

        void Deactivate(Guid widgetKey);

        void Deactivate(IWidgetIdentifier identifier);

        void Destroy(Guid widgetKey);

        void Destroy(IWidgetIdentifier identifier);
    }
}
