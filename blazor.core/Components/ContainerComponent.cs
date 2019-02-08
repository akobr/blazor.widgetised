using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Blazor.Core.Components
{
    public class ContainerComponent : ComponentBase, IContainer
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

            int sequence = 0;
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "data-key", Key);
            builder.AddContent(sequence++, content);
            builder.CloseElement();
        }
    }
}
