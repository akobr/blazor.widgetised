using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public class WidgetInlinePlatformContext : PresenterActivateInContainerContext
    {
        public RenderFragment Fragment { get; set; }
    }
}
