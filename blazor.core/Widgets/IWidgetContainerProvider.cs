using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerProvider
    {
        IRenderingContainer GetContainer(string containerKey);
    }
}
