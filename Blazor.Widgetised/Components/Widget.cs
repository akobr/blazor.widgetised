using System;
using Blazor.Widgetised.Messaging;
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
        private string VariantName { get; set; }

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
            activeWidget = Factory.Build(description)?.Mediator;
        }

        private void CreateByVariantKey()
        {
            if (string.Equals(previousVariantKey, VariantName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            previousVariantKey = VariantName;
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
            
            if (string.IsNullOrWhiteSpace(previousVariantKey))
            {
                WriteError(builder, "No variant has been specified.");
            }
            else switch (activeWidget)
            {
                case null:
                    WriteError(builder, "Specified variant has not been found.");
                    break;

                case IActivatable<Action<RenderFragment>> activation:
                    activation.Activate((f) => builder.AddContent(0, f));
                    break;

                default:
                    WriteError(builder, "Unknown widget without implementation of IActivatable<Action<RenderFragment>>.");
                    break;
            }
        }

        private static void DestroyWidget(object widget)
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

        private IWidgetManagementService Service { get; }

        public IMessageBus MessageBus { get; }

        void Foo()
        {
            // Build a widget instance through service
            WidgetInfo info = Service.Build("MyWidgetVariant");
            // Activate the widget from the container
            Service.Activate(info.Id, "MyContainer");

            // Build and activate the widget in one hit as a start message
            MessageBus.Send(new WidgetMessage.Start()
            {
                VariantName = "MyWidgetVariant",
                Position = "MyContainer"
            });
        }


    }
}
