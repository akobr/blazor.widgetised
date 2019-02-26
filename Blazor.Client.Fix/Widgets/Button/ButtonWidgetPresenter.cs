using Blazor.Widgetised.Presenters;

namespace Blazor.Client.Widgets.Button
{
    public class ButtonWidgetPresenter : WidgetPresenter<ButtonWidgetComponent>
    {
        public void SetTitle(string title)
        {
            Component?.SetTitle(title);
        }
    }
}
