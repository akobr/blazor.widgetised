namespace Blazor.Core.Widgets
{
    public interface IWidgetPresenter
    {
        void ActivateInContainer(PresenterActivateInContainerContext context);

        void ActivateInline(PresenterActivateInlineContext context);

        void Deactivate();
    }
}
