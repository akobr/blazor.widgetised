using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Button
{
    public class ButtonWidgetPresenter : WidgetPresenter<ButtonWidgetComponent>
    {
        public void SetTitle(string title)
        {
            Component.SetTitle(title);
        }
    }
}
