using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerProvider
    {
        IContainer GetContainer(string containerKey);
    }
}
