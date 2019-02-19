using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerManagement
    {
        IRenderingContainer Get(string containerKey);

        void Register(string key, IRenderingContainer container);

        void Unregister(string key);
    }
}
