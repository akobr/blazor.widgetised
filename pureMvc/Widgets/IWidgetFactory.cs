namespace Blazor.PureMvc.Widgets
{
    public interface IWidgetFactory : IFactory<IWidgetMediator, string>
    {
        void Register(string variantKey, WidgetVariant variant);
    }
}