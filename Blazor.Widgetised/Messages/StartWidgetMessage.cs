namespace Blazor.Widgetised.Messages
{
    public class StartWidgetMessage<TCustomisation> : StartWidgetMessage
        where TCustomisation : new()
    {
        public StartWidgetMessage()
        {
            Customisation = new TCustomisation();
        }

        public new TCustomisation Customisation { get; }
    }

    public class StartWidgetMessage : BuildWidgetMessage
    {
        public override void ProcessMessage(IWidgetManagementService service)
        {
            WidgetInfo = service.Start(new WidgetDescription
            {
                VariantName = VariantName,
                Variant = Variant,
                Customisation = Customisation,
                Position = Position
            });
        }
    }
}
