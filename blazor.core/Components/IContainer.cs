using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Components
{
    public interface IContainer
    {
        void SetContent(RenderFragment component);
    }
}
