namespace Blazor.Widgetised
{
    public class WidgetDescription
    {
        public string VariantKey { get; set; }

        public WidgetVariant Variant { get; set; }

        public object Customisation { get; set; }

        public string Position { get; set; }
    }
}
