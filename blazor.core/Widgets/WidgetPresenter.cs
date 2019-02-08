using Blazor.Core.Components;
using Blazor.PureMvc;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public abstract class WidgetPresenter : IPresenter
    {
        private readonly IWidgetContainerProvider provider;
        private IComponentContainer container;

        protected abstract IComponent Component { get; }

        public void Activate(string containerKey)
        {
            container = provider.GetContainer(containerKey);
            container?.SetContent(Component);
        }

        public void Deactivate()
        {
            container?.SetContent(null);
        }
    }
}
