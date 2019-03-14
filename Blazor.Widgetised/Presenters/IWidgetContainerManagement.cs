using Blazor.Widgetised.Components;

namespace Blazor.Widgetised.Presenters
{
    public interface IWidgetContainerManagement
    {
        IRenderingContainer? Get(string containerKey);

        void Register(string key, IRenderingContainer container);

        void Unregister(string key);
    }
}
