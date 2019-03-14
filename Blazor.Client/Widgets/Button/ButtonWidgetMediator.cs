using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messaging;

namespace Blazor.Client.Widgets.Button
{
    public class ButtonWidgetMediator : WidgetMediator<ButtonWidgetPresenter>, IInitialisable
    {
        private ButtonWidgetCustomisation? customisation;

        public void Initialise()
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
