using System;

namespace Blazor.Client.Widgets.Button
{
    public class ButtonWidgetCustomisation
    {
        public string? Title { get; set; }

        public Action? ClickStrategy { get; set; }
    }
}
