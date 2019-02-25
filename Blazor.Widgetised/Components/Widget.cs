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
        private string Variant { get; set; }

        [Parameter]
        private string Position { get; set; }

        [Parameter]
        private WidgetDescription Description
        {
            get => description;
            set
            {
                if (ReferenceEquals(description, value))
                {
                    return;
                }

                description = value;

                if (!parametersHasBeenSet)
                {
                    return;
                }

                CreateByDescriptionModel();
            }
        }

        public void SetPosition(string newPosition)
        {
            Position = newPosition;
        }

        public void SetDescription(WidgetDescription newDescription)
        {
            Description = newDescription;
            StateHasChanged();
        }

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
                CreateByDescriptionModel();
                return;
            }

            CreateByVariantKey();
        }

        private void CreateByDescriptionModel()
        {
            if (ReferenceEquals(previousDescription, description))
            {
                return;
            }

            previousDescription = description;
            DestroyWidget(activeWidget);
            (_, activeWidget) = Factory.Build(description);
        }

        private void CreateByVariantKey()
        {
            if (string.Equals(previousVariantKey, Variant, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            previousVariantKey = Variant;
            DestroyWidget(activeWidget);
            (_, activeWidget) = Factory.Build(new WidgetDescription
            {
                VariantName = Variant,
                Position = Position
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
