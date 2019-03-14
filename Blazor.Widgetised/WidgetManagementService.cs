using System;
using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised
{
    public class WidgetManagementService : IWidgetManagementService
    {
        private readonly IWidgetStore store;
        private readonly IWidgetFactory factory;
        private readonly IMessageBus messageBus;
        private readonly ILogger logger;

        public WidgetManagementService(
            IWidgetStore store,
            IWidgetFactory factory,
            IMessageBus messageBus,
            ILogger logger)
        {
            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            this.logger = logger;

            RegisterWithMessageBus();
        }

        public WidgetInfo Build(string variantName)
        {
            return Build(new WidgetDescription {VariantName = variantName});
        }

        public WidgetInfo Build(WidgetVariant variant)
        {
            return Build(new WidgetDescription {Variant = variant});
        }

        public WidgetInfo Build(WidgetDescription description)
        {
            return factory.Build(description);
        }

        public WidgetInfo Start(string variantName, string containerKey)
        {
            return Start(new WidgetDescription {VariantName = variantName, Position = containerKey});
        }

        public WidgetInfo Start(WidgetDescription description)
        {
            WidgetInfo info = Build(description);

            if (!string.IsNullOrEmpty(description.Position))
            {
                ActivateMediator(info.Mediator, description.Position);
            }

            return info;
        }

        public void Activate(Guid widgetId, string containerKey)
        {
            if (widgetId == Guid.Empty)
            {
                throw new ArgumentException("Empty widget id.", nameof(widgetId));
            }

            if (string.IsNullOrEmpty(containerKey))
            {
                throw new ArgumentNullException(nameof(containerKey));
            }

            object? mediator = store.Get(widgetId);

            if (mediator == null)
            {
                logger.Warning($"Widget {widgetId:B} can't be activated because doesn't exist.");
                return;
            }

            ActivateMediator(mediator, containerKey);
        }

        public void Deactivate(Guid widgetId)
        {
            if (widgetId == Guid.Empty)
            {
                throw new ArgumentException("Empty widget id.", nameof(widgetId));
            }

            object? mediator = store.Get(widgetId);

            if (mediator == null)
            {
                logger.Warning($"Widget {widgetId:B} can't be deactivated because doesn't exist.");
                return;
            }

            if (mediator is WidgetMediator knownMediator)
            {
                knownMediator.Deactivate();
            }
        }

        public void Destroy(Guid widgetId)
        {
            if (widgetId == Guid.Empty)
            {
                throw new ArgumentException("Empty widget id.", nameof(widgetId));
            }

            object? mediator = store.Get(widgetId);

            if (mediator == null)
            {
                logger.Warning($"Widget {widgetId:B} can't be destroyed because doesn't exist.");
                return;
            }

            if (mediator is IDisposable disposable)
            {
                disposable.Dispose();
            }

            store.Remove(widgetId);
        }

        private void RegisterWithMessageBus()
        {
            messageBus.Register<IWidgetMessage>(this, ProcessWidgetMesage);
        }

        private void ProcessWidgetMesage(IWidgetMessage message)
        {
            message.ProcessMessage(this);
        }

        private static void ActivateMediator(object mediator, string containerKey)
        {
            if (mediator is WidgetMediator knownMediator)
            {
                knownMediator.Activate(containerKey);
            }
        }
    }
}
