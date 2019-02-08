using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerProvider
    {
        IComponentContainer GetContainer(string containerKey);
    }
}
