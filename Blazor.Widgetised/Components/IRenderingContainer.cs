using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Components
{
    public interface IRenderingContainer
    {
        void SetContent(RenderFragment component);
    }
}
