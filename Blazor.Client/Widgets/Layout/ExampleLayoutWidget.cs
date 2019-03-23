using System;
using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messages;
using Blazor.Widgetised.Messaging;

namespace Blazor.Client.Widgets.Layout
{
    public class ExampleLayoutWidget : RazorWidgetMediator<ExampleLayout, LayoutState>, IInitialisable
    {
        private readonly IMessageBus messageBus;

        public ExampleLayoutWidget(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public void Initialise()
        {
            InteractionPipe.Register<Messages.Rendered>(HandleFirstRender);
            InteractionPipe.Register<LayoutOperationMessage>(HandleOperation);
        }

        protected override void OnDestroy()
        {
            foreach (LayoutState.LayoutItem item in State.Widgets.Values)
            {
                DestroyWidget(item.WidgetId);
            }

            messageBus.UnregisterAll(this);
        }

        // Restore last state of the layout
        private void HandleFirstRender(Messages.Rendered obj)
        {
            foreach (LayoutState.LayoutItem item in State.Widgets.Values)
            {
                messageBus.Send(new StartWidgetMessage { VariantName = item.VariantName, Position = item.ContainerKey });
            }
        }

        private void HandleOperation(LayoutOperationMessage message)
        {
            if (message.TargetWidgetVariant == "delete")
            {
                if (State.Widgets.TryGetValue(message.TargetContainer, out var layout))
                {
                    DestroyWidget(layout.WidgetId);
                    State.Widgets.Remove(message.TargetContainer);
                }
            }
            else
            {
                CreateWidget(message.TargetWidgetVariant, message.TargetContainer);
            }
        }

        private void CreateWidget(string widgetName, string position)
        {
            StartWidgetMessage message = new StartWidgetMessage
            {
                VariantName = widgetName,
                Position = position
            };

            messageBus.Send(message);

            if (message.WidgetInfo == null)
            {
                throw new InvalidOperationException($"The widget [{widgetName}] hasn't been started.");
            }

            State.Widgets[position] = new LayoutState.LayoutItem(message.WidgetInfo.Id, widgetName, position);
        }

        private void DestroyWidget(Guid widgetId)
        {
            messageBus.Send(new DestroyWidgetMessage(widgetId));
        }
    }
}
