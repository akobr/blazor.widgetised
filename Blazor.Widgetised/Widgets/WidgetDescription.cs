namespace Blazor.Widgetised
{
    public class WidgetDescription : IWidgetIdentifier
    {
        public string VariantName { get; set; }

        public WidgetVariant Variant { get; set; }

        public string Position { get; set; }

        public object Customisation { get; set; }

        string IWidgetIdentifier.Key => WidgetIdentifier.BuildKey(VariantName, Position);
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
