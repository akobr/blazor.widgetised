using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Components
{
    public interface IComponentContainer
    {
        void SetContent(IComponent component);
    }
}
