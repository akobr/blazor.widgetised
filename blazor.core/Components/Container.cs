using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Blazor.Core.Components
{
    public class Container : ComponentBase, IComponentContainer
    {
        private IComponent content;

        public void SetContent(IComponent component)
        {
            // TODO: dispose old one?
            content = component;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            int sequence = 0;
            builder.OpenComponent(++sequence, content.GetType());
            builder.CloseComponent();
        }
    }
}
