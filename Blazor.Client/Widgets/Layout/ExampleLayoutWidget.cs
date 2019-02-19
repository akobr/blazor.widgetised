using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;

namespace Blazor.Client.Widgets.Layout
{
    public class ExampleLayoutWidget : BlazorWidgetMediator<ExampleLayout>, IInitialisable
    {
        public void Initialise()
        {
            InteractionPipe.Register<LayoutOperationMessage>(HandleOperation);
        }

        private void HandleOperation(LayoutOperationMessage message)
        {
            if (message.TargetWidgetVariant == "delete")
            {
                MessageBus.Send(new WidgetMessage.Destroy
                {
                    VariantName = message.TargetWidgetVariant,
                    Position = message.TargetContainer
                });
            }

            MessageBus.Send(new WidgetMessage.Build
            {
                VariantName = message.TargetWidgetVariant,
                Position = message.TargetContainer
            });

            MessageBus.Send(new WidgetMessage.Activate
            {
                VariantName = message.TargetWidgetVariant,
                Position = message.TargetContainer
            });
        }
    }
}
