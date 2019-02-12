namespace Blazor.Core.Widgets
{
    public interface IWidgetFactory : IFactory<object, string>
    {
        void Register(string variantKey, WidgetVariant variant);

        object Build(WidgetVariant variant);
    }
}