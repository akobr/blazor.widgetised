using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Text
{
    public class TextWidgetMediator : BlazorWidgetMediator<TextComponent>
    {
        protected override void InitialRender()
        {
            Component.SetText(GetCustomisation<TextWidgetCustomisation>().Text);
        }
    }
}
