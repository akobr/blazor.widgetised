using Blazor.Widgetised.Mediators;

namespace Blazor.Client.Widgets.Text
{
    public class TextWidgetMediator : RazorWidgetMediator<TextComponent>
    {
        protected override void InitialRender()
        {
            Component.SetText(GetCustomisation<TextWidgetCustomisation>()?.Text ?? "No customisation model");
        }
    }
}
