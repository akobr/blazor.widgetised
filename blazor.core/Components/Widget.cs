using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;

namespace Blazor.Core.Components
{
    public class Widget : ComponentBase, IDisposable
    {
        private object activeWidget;
        private string previousVariant;

        [Inject]
        protected IWidgetFactory Factory { get; set; }

        [Parameter]
        protected string Variant { get; set; }

        public void Dispose()
        {
            DestroyWidget(activeWidget);
            activeWidget = null;
        }

        protected override void OnParametersSet()
        {
            if (string.Equals(previousVariant, Variant, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            previousVariant = Variant;
            DestroyWidget(activeWidget);
            activeWidget = Factory.Build(Variant);
        }

        private void DestroyWidget(object widget)
        {
            if (widget == null)
            {
                return;
            }

            ((IActivatable<Action<RenderFragment>>)widget).Deactivate();

            if (widget is IDisposable dispose)
            {
                dispose.Dispose();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            
            if (string.IsNullOrWhiteSpace(previousVariant))
            {
                WriteError(builder, "No variant has been specified.");
            }
            else if (activeWidget == null)
            {
                WriteError(builder, "Specified variant has not been found.");
            }
            else if (activeWidget is IActivatable<Action<RenderFragment>> activation)
            {
                activation.Activate((f) => builder.AddContent(0, f));
            }
            else
            {
                WriteError(builder, "Unknown widget without implementation of IActivatable<Action<RenderFragment>>.");
            }
        }

        private static void WriteError(RenderTreeBuilder builder, string message)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "error");
            builder.OpenElement(2, "em");
            builder.AddContent(2, message);
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}
