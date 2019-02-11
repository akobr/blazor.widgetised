using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Components
{
    public class Widget : ComponentBase
    {
        [Inject]
        protected IWidgetFactory Factory { get; set; }

        [Parameter]
        protected string Variant { get; set; }
    }
}
