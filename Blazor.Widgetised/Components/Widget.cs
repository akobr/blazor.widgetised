using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Blazor.Widgetised.Components
{
    public class Widget : ComponentBase, IDisposable
    {
        private object? activeWidget;
        private string? activeVariantName;
        private WidgetDescription? activeDescription;
        private WidgetDescription? description;
        private bool parametersHasBeenSet;

        public Widget()
        {
            Factory = NotAssigned.WidgetFactory;
        }

        [Inject]
        protected IWidgetFactory Factory { get; set; }

        [Parameter]
        private string? VariantName { get; set; }

        [Parameter]
        private string? Position { get; set; }

        [Parameter]
        private WidgetDescription? Description
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
            if (ReferenceEquals(activeDescription, description))
            {
                return;
            }

            activeDescription = description;
            DestroyWidget(activeWidget);

            if (description == null)
            {
                return;
            }

            activeWidget = Factory.Build(description)?.Mediator;
        }

        private void CreateByVariantKey()
        {
            if (string.Equals(activeVariantName, VariantName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            activeVariantName = VariantName;
            DestroyWidget(activeWidget);
            activeWidget = Factory.Build(new WidgetDescription
            {
                VariantName = VariantName,
                Position = Position
            })?.Mediator;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
                        
            switch (activeWidget)
            {
                case IActivatable<Action<RenderFragment>> activation:
                    activation.Activate((f) => builder.AddContent(0, f));
                    break;

                case null:
                    if (activeDescription != null)
                    {
                        WriteError(builder, "Specified widget description didn't produce a widget.");
                    }
                    else if (string.IsNullOrWhiteSpace(activeVariantName))
                    {
                        WriteError(builder, "No variant has been specified.");
                    }
                    else
                    {
                        WriteError(builder, $"Specified variant [{activeVariantName}] has not been found.");
                    }
                    break;

                default:
                    WriteError(builder, "Unknown widget type without implementation of IActivatable<Action<RenderFragment>>.");
                    break;
            }
        }

        private static void DestroyWidget(object? widget)
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
