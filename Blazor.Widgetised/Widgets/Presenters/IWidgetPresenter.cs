namespace Blazor.Widgetised.Presenters
{
    public interface IWidgetPresenter
    {
        void Activate(IPresenterInlineActivationContext context);

        void Activate(IPresenterInContainerActivationContext context);

        void Deactivate();
    }
}
