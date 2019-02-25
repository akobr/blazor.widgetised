namespace Blazor.Widgetised
{
    public class WidgetDescription : IWidgetIdentifier
    {
        public string VariantName { get; set; }

        public WidgetVariant Variant { get; set; }

        public string Position { get; set; }

        public object Customisation { get; set; }

        string IWidgetIdentifier.Name => new WidgetIdentifier(VariantName, Position).Name;

        string IWidgetIdentifier.Position => new WidgetIdentifier(VariantName, Position).Position;

        string IWidgetIdentifier.GetKey()
        {
            return new WidgetIdentifier(VariantName, Position).GetKey();
        }
    }

    public class WidgetDescription<TCustomisation> : WidgetDescription
        where TCustomisation : new()
    {
        public WidgetDescription()
        {
            Customisation = new TCustomisation();
        }

        public new TCustomisation Customisation { get; set; }
    }
}
