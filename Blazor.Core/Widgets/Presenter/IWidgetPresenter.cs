namespace Blazor.Core.Widgets
{
    public interface IWidgetPresenter
    {
        void Activate(IPresenterInlineActivationContext context);

        void Activate(IPresenterInContainerActivationContext context);

        void Deactivate();
    }
}
