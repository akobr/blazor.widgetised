using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Blazor.Core.Components
{
    public class ContainerComponent : ComponentBase, IRenderingContainer
    {
        private RenderFragment content;
        private string key;

        public string Key
        {
            get { return key; }

            set
            {
                if (string.Equals(key, value, System.StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                key = value;
                StateHasChanged();
            }
        }

        public void SetContent(RenderFragment content)
        {
            this.content = content;
            StateHasChanged();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "container");
            builder.AddAttribute(2, "data-key", Key);
            
            if (content != null)
            {
                builder.AddContent(3, content);
            }

            builder.CloseElement();
        }
    }
}
