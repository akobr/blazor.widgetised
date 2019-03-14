namespace Blazor.Widgetised.Messages
{
    public class BuildWidgetMessage<TCustomisation> : BuildWidgetMessage
        where TCustomisation : new()
    {
        public BuildWidgetMessage()
        {
            Customisation = new TCustomisation();
        }

        public new TCustomisation Customisation { get; }
    }

    public class BuildWidgetMessage : IWidgetMessage
    {
        public string? VariantName { get; set; }

        public WidgetVariant? Variant { get; set; }

        public object? Customisation { get; set; }

        public string? Position { get; set; }

        public WidgetInfo? WidgetInfo { get; protected set; }

        public virtual void ProcessMessage(IWidgetManagementService service)
        {
            WidgetInfo = service.Build(new WidgetDescription
            {
                VariantName = VariantName,
                Variant = Variant,
                Customisation = Customisation,
                Position = Position
            });
        }
    }
}
