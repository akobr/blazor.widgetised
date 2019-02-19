namespace Blazor.Widgetised
{
    public interface IWidgetFactory : IFactory<object, string>
    {
        void Register(string variantKey, WidgetVariant variant);

        object Build(WidgetDescription description);
    }
}