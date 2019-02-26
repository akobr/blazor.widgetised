using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messaging;

namespace Blazor.Client.Widgets.Layout
{
    public class ExampleLayoutWidget : BlazorWidgetMediator<ExampleLayout, LayoutState>, IInitialisable
    {
        public void Initialise()
        {
            InteractionPipe.Register<Messages.Rendered>(HandleFirstRender);
            InteractionPipe.Register<LayoutOperationMessage>(HandleOperation);
        }


        protected override void OnDestroy()
        {
            foreach (LayoutState.LayoutItem item in State.Widgets.Values)
            {
                DestroyWidget(item.VariantName, item.ContainerKey);
            }
        }

        // Restore last state of the layout
        private void HandleFirstRender(Messages.Rendered obj)
        {
            foreach (LayoutState.LayoutItem item in State.Widgets.Values)
            {
                MessageBus.Send(new WidgetMessage.Start { VariantName = item.VariantName, Position = item.ContainerKey });
            }
        }

        private void HandleOperation(LayoutOperationMessage message)
        {
            if (message.TargetWidgetVariant == "delete")
            {
                DestroyWidget(message.TargetWidgetVariant, message.TargetContainer);
                State.Widgets.Remove(message.TargetContainer);
            }
            else
            {
                CreateWidget(message.TargetWidgetVariant, message.TargetContainer);
            }
        }

        private void CreateWidget(string widgetName, string position)
        {
            MessageBus.Send(new WidgetMessage.Start
            {
                VariantName = widgetName,
                Position = position
            });

            State.Widgets[position] = new LayoutState.LayoutItem()
            {
                VariantName = widgetName,
                ContainerKey = position
            };
        }

        private void DestroyWidget(string widgetName, string position)
        {
            MessageBus.Send(new WidgetMessage.Destroy
            {
                VariantName = widgetName,
                Position = position
            });
        }
    }
}
