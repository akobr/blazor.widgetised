using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Components
{
    public interface IRenderingContainer
    {
        void SetContent(RenderFragment component);
    }
}
