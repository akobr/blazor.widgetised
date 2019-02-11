namespace Blazor.PureMvc.Widgets
{
    public interface IWidgetFactory : IFactory<object, string>
    {
        void Register(string variantKey, WidgetVariant variant);
    }
}