using Blazor.Core.Components;
using Blazor.PureMvc;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public abstract class WidgetPresenter<TComponent> : IPresenter
        where TComponent : class, IComponent
    {
        private readonly IWidgetContainerProvider provider;
        private IContainer container;

        public TComponent Component { get; private set; }

        public void Activate(string containerKey)
        {
            container = provider.GetContainer(containerKey);
            container?.SetContent(BuildFragment());
        }

        public void Deactivate()
        {
            container?.SetContent(null);
        }

        protected virtual RenderFragment BuildFragment()
        {
            return (builder) =>
            {
                builder.OpenComponent<TComponent>(0);
                builder.AddComponentReferenceCapture(1, (comRef) => Component = (TComponent)comRef);
                builder.CloseComponent();
            };
        }
    }
}
