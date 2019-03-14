using System;
using Blazor.Widgetised.Components;
using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Messaging;
using Blazor.Widgetised.Presenters;

namespace Blazor.Widgetised
{
    internal static class NotAssigned
    {
        public readonly static IWidgetContainerManagement WidgetContainerManagement = new NotAssignedWidgetContainerManagement();
        public readonly static IWidgetFactory WidgetFactory = new NotAssignedWidgetFactory();
        public readonly static IMessageBus MessageBus = new NotAssignedMessageBus();
        public readonly static ILogger Logger = new NotAssignedLogger();

        private static Exception CreateException(string name)
        {
            return new InvalidOperationException($"Try to use non-assigned {name}.");
        }

        private sealed class NotAssignedWidgetContainerManagement : IWidgetContainerManagement
        {
            public IRenderingContainer? Get(string containerKey)
            {
                throw CreateException(nameof(IWidgetContainerManagement));
            }

            public void Register(string key, IRenderingContainer container)
            {
                throw CreateException(nameof(IWidgetContainerManagement));
            }

            public void Unregister(string key)
            {
                throw CreateException(nameof(IWidgetContainerManagement));
            }
        }

        private sealed class NotAssignedWidgetFactory : IWidgetFactory
        {
            public WidgetInfo Build(WidgetDescription description)
            {
                throw CreateException(nameof(IWidgetFactory));
            }

            public WidgetInfo Build(string variantName)
            {
                throw CreateException(nameof(IWidgetFactory));
            }

            public void Register(string variantName, WidgetVariant variant)
            {
                throw CreateException(nameof(IWidgetFactory));
            }
        }

        private sealed class NotAssignedMessageBus : IMessageBus
        {
            public void Register<TMessage>(object receiver, Action<TMessage> handler)
                where TMessage : IMessage
            {
                throw CreateException(nameof(IMessageBus));
            }

            public void Send<TMessage>(TMessage message)
                where TMessage : IMessage
            {
                throw CreateException(nameof(IMessageBus));
            }

            public void Unregister<TMessage>(object receiver)
            {
                throw CreateException(nameof(IMessageBus));
            }

            public void UnregisterAll(object receiver)
            {
                throw CreateException(nameof(IMessageBus));
            }
        }

        private sealed class NotAssignedLogger : ILogger
        {
            public void Log(LogLevel level, string message, params object[] args)
            {
                throw CreateException(nameof(ILogger));
            }
        }
    }
}
