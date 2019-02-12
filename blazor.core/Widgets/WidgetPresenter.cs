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

        public void ActivateInContainer(PresenterActivateInContainerContext context)
        {
            interactionPipe = context.InteractionPipe;
            container = containerManagement.Get(context.ContainerKey);
            container?.SetContent(BuildFragment(context.ContinueWith));
        }

        public void ActivateInline(PresenterActivateInlineContext context)
        {
            interactionPipe = context.InteractionPipe;
            context.RenderAction?.Invoke(BuildFragment(context.ContinueWith));
        }

        public void Deactivate()
        {
            container?.SetContent(null);
            container = null;
        }

        void IWidgetPresenterBuildContract.SetWidgetContainerManagement(IWidgetContainerManagement newContainerManagement)
        {
            containerManagement = newContainerManagement;
        }

        protected virtual RenderFragment BuildFragment(Action continueActivation)
        {
            return (builder) =>
            {
                builder.OpenComponent<TComponent>(0);
                builder.AddComponentReferenceCapture(1, (componentRef) =>
                {
                    TComponent component = (TComponent)componentRef;
                    Component = component;
                    TryFillContract(component);
                    continueActivation?.Invoke();

                });
                builder.CloseComponent();
            };
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
