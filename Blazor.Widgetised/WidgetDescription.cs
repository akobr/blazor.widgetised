namespace Blazor.Widgetised
{
    public class WidgetDescription
    {
        public string? VariantName { get; set; }

        public WidgetVariant? Variant { get; set; }

        public string? Position { get; set; }

        public object? Customisation { get; set; }

        internal static (bool isFull, string key) BuildKey(WidgetDescription description)
        {
            bool isFull = !string.IsNullOrEmpty(description.Position);
            string name = description.Variant?.MediatorType.Name ?? "UNKNOWN";

            return isFull
                    ? (true, $"{name}|{description.Position}")
                    : (false, name);
        }
    }

    public class WidgetDescription<TCustomisation> : WidgetDescription
        where TCustomisation : new()
    {
        public WidgetDescription()
        {
            Customisation = new TCustomisation();
            base.Customisation = Customisation;
        }

        public new TCustomisation Customisation { get; }
    }
}
