using System;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised
{
    public class WidgetManagementService : IWidgetManagementService
    {
        private readonly IWidgetStore store;
        private readonly IWidgetFactory factory;
        private readonly IMessageBus messageBus;

        public WidgetManagementService(IWidgetStore store, IWidgetFactory factory, IMessageBus messageBus)
        {
            this.store = store;
            this.factory = factory;
            this.messageBus = messageBus;

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

            if (info == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(description.Position))
            {
                ActivateMediator(info.Mediator, description.Position);
            }

            return info;
        }

        public void Activate(Guid widgetId, string containerKey)
        {
            if (widgetId == Guid.Empty
                || string.IsNullOrEmpty(containerKey))
            {
                return;
            }

            ActivateMediator(store.Get(widgetId), containerKey);
        }

        public void Activate(IWidgetIdentifier identifier, string containerKey)
        {
            if (identifier == null)
            {
                return;
            }

            Guid id = store.GetGuid(identifier.Key);
            Activate(id, containerKey);
        }

        public void Deactivate(Guid widgetId)
        {
            if (widgetId == Guid.Empty)
            {
                return;
            }

            WidgetMediator mediator = store.Get(widgetId) as WidgetMediator;
            mediator?.Deactivate();
        }

        public void Deactivate(IWidgetIdentifier identifier)
        {
            if (identifier == null)
            {
                return;
            }

            Guid id = store.GetGuid(identifier.Key);
            Deactivate(id);
        }

        public void Destroy(Guid widgetId)
        {
            if (widgetId == Guid.Empty)
            {
                return;
            }

            store.Remove(widgetId);
        }

        public void Destroy(IWidgetIdentifier identifier)
        {
            if (identifier == null)
            {
                return;
            }

            Guid id = store.GetGuid(identifier.Key);
            Destroy(id);
        }

        private void RegisterWithMessageBus()
        {
            messageBus.Register<WidgetMessage.Build>(this, ProcessBuildMessage);
            messageBus.Register<WidgetMessage.Activate>(this, ProcessActivateMessage);
            messageBus.Register<WidgetMessage.Deactivate>(this, ProcessDeactivateMessage);
            messageBus.Register<WidgetMessage.Destroy>(this, ProcessDestroyMessage);
        }

        private void ProcessBuildMessage(WidgetMessage.Build message)
        {
            if (message == null)
            {
                return;
            }

            WidgetInfo info = Build(new WidgetDescription
            {
                VariantName = message.VariantName,
                Variant = message.Variant,
                Customisation = message.Customisation,
                Position = message.Position
            });

            message.WidgetId = info.Id;
            message.WidgetKey = info.Key;
        }

        private void ProcessActivateMessage(WidgetMessage.Activate message)
        {
            if (message == null
                || string.IsNullOrEmpty(message.Position))
            {
                return;
            }

            if (message.WidgetId != Guid.Empty)
            {
                Activate(message.WidgetId, message.Position);
            }
            else if (string.IsNullOrEmpty(message.WidgetKey))
            {
                Activate(store.GetGuid(message.WidgetKey), message.Position);
            }
            else
            {
                Activate(store.GetGuid(((IWidgetIdentifier)message).Key), message.Position);
            }
        }

        private void ProcessDeactivateMessage(WidgetMessage.Deactivate message)
        {
            if (message == null)
            {
                return;
            }

            if (message.WidgetId != Guid.Empty)
            {
                Deactivate(message.WidgetId);
            }
            else if (string.IsNullOrEmpty(message.WidgetKey))
            {
                Deactivate(store.GetGuid(message.WidgetKey));
            }
            else
            {
                Deactivate(store.GetGuid(((IWidgetIdentifier)message).Key));
            }
        }

        private void ProcessDestroyMessage(WidgetMessage.Destroy message)
        {
            if (message == null)
            {
                return;
            }

            if (message.WidgetId != Guid.Empty)
            {
                Destroy(message.WidgetId);
            }
            else if (string.IsNullOrEmpty(message.WidgetKey))
            {
                Destroy(store.GetGuid(message.WidgetKey));
            }
            else
            {
                Destroy(store.GetGuid(((IWidgetIdentifier)message).Key));
            }
        }

        private static void ActivateMediator(object mediator, string containerKey)
        {
            WidgetMediator typedMediator = mediator as WidgetMediator;
            typedMediator?.Activate(containerKey);
        }
    }
}
