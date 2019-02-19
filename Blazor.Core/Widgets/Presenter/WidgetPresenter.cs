using System;
using Blazor.Core.Components;
using Blazor.Core.Interactions;
using Blazor.Core.Logging;
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

        public void Activate(IPresenterInlineActivationContext context)
        {
            if (context == null)
            {
                return;
            }

            interactionPipe = context.InteractionPipe;
            context.RenderAction?.Invoke(BuildFragment(context.ContinueWith));
        }

        public void Activate(IPresenterInContainerActivationContext context)
        {
            if (context == null)
            {
                return;
            }

            interactionPipe = context.InteractionPipe;
            container = containerManagement.Get(context.ContainerKey);

            if (container == null)
            {
                ConsoleLogger.Debug($"WARNING: No container for key '{context.ContainerKey}' has been found.");
            }

            container?.SetContent(BuildFragment(context.ContinueWith));
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

        protected virtual RenderFragment BuildFragment(Action continueWith)
        {
            return (builder) =>
            {
                builder.OpenComponent<TComponent>(0);
                builder.AddComponentReferenceCapture(1, (componentRef) =>
                {
                    TComponent component = (TComponent)componentRef;
                    Component = component;
                    TryFillContract(component);
                    continueWith?.Invoke();

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
