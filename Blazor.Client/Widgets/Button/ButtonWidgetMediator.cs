using Blazor.Core.Messaging;
using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Button
{
    public class ButtonWidgetMediator : WidgetMediator<ButtonWidgetPresenter>
    {
        private ButtonWidgetCustomisation customisation;

        protected override void OnInitialise()
        {
            customisation = GetCustomisation<ButtonWidgetCustomisation>();
            InteractionPipe.Register<Messages.Click>(HandleButtonClick);
        }

        protected override void InitialRender()
        {
            Presenter.SetTitle(customisation?.Title ?? "unknown");
        }

        private void HandleButtonClick(Messages.Click message)
        {
            customisation?.ClickStrategy?.Invoke();
        }
    }
}
