using Blazor.Core.Logging;
using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;

namespace Blazor.Core.Components
{
    public class Container : ComponentBase, IRenderingContainer, IDisposable
    {
        private RenderFragment content;
        private string registeredKey;

        [Inject]
        private IWidgetContainerManagement Management { get; set; }

        [Parameter]
        private string Key { get; set; }

        [Parameter]
        private RenderFragment ChildContent { get; set; }

        public void SetKey(string newKey)
        {
            if (IsKeySameLastPrevious(newKey))
            {
                return;
            }

            Key = newKey;
            RegisterKey(newKey);
            StateHasChanged();
        }

        public void SetContent(RenderFragment content)
        {
            this.content = content;
            StateHasChanged();
        }

        public void Dispose()
        {
            UnregisterPreviousKey();
        }

        protected override void OnParametersSet()
        {
            RegisterKey(Key);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "system-container");

            if (!string.IsNullOrEmpty(Key))
            {
                builder.AddAttribute(2, "data-key", Key);
            }

            if (content != null)
            {
                builder.AddContent(3, content);
            }
            else if (ChildContent != null)
            {
                builder.AddContent(3, ChildContent);
            }

            builder.CloseElement();
        }

        private void RegisterKey(string key)
        {
            if (IsKeySameLastPrevious(key))
            {
                return;
            }

            UnregisterPreviousKey();

            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            ConsoleLogger.Debug($"DEBUG: Container with key '{key}' is registering.");
            Management.Register(key, this);
            registeredKey = key;
        }

        private void UnregisterPreviousKey()
        {
            if (registeredKey == null)
            {
                return;
            }

            Management.Unregister(registeredKey);
            registeredKey = null;
        }

        private bool IsKeySameLastPrevious(string key)
        {
            return string.Equals(key, registeredKey, StringComparison.OrdinalIgnoreCase);
        }
    }
}
