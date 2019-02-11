using System;
using Blazor.Core.Components;
using Blazor.Core.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public class WidgetPresenter<TComponent> : IWidgetPresenter, IWidgetPresenterBuildContract
        where TComponent : class, IComponent
    {
        private IWidgetContainerManagement containerManagement;
        private IInteractionPipe interactionPipe;
        private IRenderingContainer container;

        public TComponent Component { get; private set; }

        public void Activate(WidgetPlatformContext context)
        {
            interactionPipe = context.InteractionPipe;
            container = containerManagement.Get(context.ContainerKey);
            container?.SetContent(BuildFragment());
        }

        public void Activate(Action<RenderFragment> fragmentAction)
        {
            fragmentAction?.Invoke(BuildFragment());
        }

        public void Deactivate()
        {
            container?.SetContent(null);
        }

        void IWidgetPresenterBuildContract.SetWidgetContainerManagement(IWidgetContainerManagement newContainerManagement)
        {
            containerManagement = newContainerManagement;
        }

        protected virtual RenderFragment BuildFragment()
        {
            return (builder) =>
            {
                builder.OpenComponent<TComponent>(0);
                builder.AddComponentReferenceCapture(1, RegisterComponent);
                builder.CloseComponent();
            };
        }

        private void RegisterComponent(object componentRef)
        {
            TComponent component = (TComponent)componentRef;
            Component = component;

            TryFillContract(component);
        }

        private void TryFillContract(TComponent component)
        {
            if (!(component is IComponentBuildContract contract))
            {
                return;
            }

            contract.SetInteractionPipe(new InteractionPipe(interactionPipe));
        }
    }
}
