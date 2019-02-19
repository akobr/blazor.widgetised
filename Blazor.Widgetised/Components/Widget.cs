using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Blazor.Widgetised.Components
{
    public class Widget : ComponentBase, IDisposable
    {
        private object activeWidget;
        private string previousVariantKey;
        private WidgetDescription previousDescription;
        private WidgetDescription description;
        private bool parametersHasBeenSet;

        [Inject]
        protected IWidgetFactory Factory { get; set; }

        [Parameter]
        protected string Variant { get; set; }

        public WidgetDescription Description
        {
            get { return description; }
            set
            {
                if (ReferenceEquals(description, value))
                {
                    return;
                }

                description = value;

                if (!parametersHasBeenSet || !TryCreateByVariantModel())
                {
                    return;
                }

                StateHasChanged();
            }
        }

        public string PositionKey { get; set; }

        public void Dispose()
        {
            DestroyWidget(activeWidget);
            activeWidget = null;
        }

        protected override void OnParametersSet()
        {
            parametersHasBeenSet = true;

            if (description != null)
            {
                TryCreateByVariantModel();
                return;
            }

            CreateByVariantKey();
        }

        private bool TryCreateByVariantModel()
        {
            if (ReferenceEquals(previousDescription, description))
            {
                return false;
            }

            previousDescription = description;
            DestroyWidget(activeWidget);
            activeWidget = Factory.Build(description);
            return true;
        }

        private void CreateByVariantKey()
        {
            if (string.Equals(previousVariantKey, Variant, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            previousVariantKey = Variant;
            DestroyWidget(activeWidget);
            activeWidget = Factory.Build(new WidgetDescription
            {
                VariantKey = Variant,
                Position = PositionKey
            });
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
            
            if (string.IsNullOrWhiteSpace(previousVariantKey))
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
