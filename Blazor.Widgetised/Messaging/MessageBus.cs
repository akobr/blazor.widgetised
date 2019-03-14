using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Widgetised.Logging;

namespace Blazor.Widgetised.Messaging
{
    public class MessageBus : IMessageBus
    {
        private readonly static Type baseType = typeof(IMessage);

        private readonly ILogger logger;
        private readonly IDictionary<Type, IDictionary<object, Delegate>> mapByMessage;
        private readonly IDictionary<object, IDictionary<Type, Delegate>> mapByReceiver;

        public MessageBus(ILogger logger)
        {
            this.logger = logger;
            mapByMessage = new Dictionary<Type, IDictionary<object, Delegate>>();
            mapByReceiver = new Dictionary<object, IDictionary<Type, Delegate>>();
        }

        public void Register<TMessage>(object receiver, Action<TMessage> handler)
            where TMessage : IMessage
        {
            if (receiver == null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!mapByReceiver.TryGetValue(receiver, out var methods))
            {
                methods = new Dictionary<Type, Delegate>();
                mapByReceiver[receiver] = methods;
            }

            Type messageType = typeof(TMessage);

            if (!mapByMessage.TryGetValue(messageType, out var receivers))
            {
                receivers = new Dictionary<object, Delegate>();
                mapByMessage[messageType] = receivers;
            }

            methods[messageType] = handler;
            receivers[receiver] = handler;
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);
            logger.Trace($"Message [{messageType.Name}] has been sent.");
            Send(message, messageType);
            SendAsInterfaces(message, messageType);
        }

        public void Unregister<TMessage>(object receiver)
        {
            if (!mapByReceiver.TryGetValue(receiver, out var messages))
            {
                return;
            }

            Type mesageType = typeof(TMessage);

            if (messages.Remove(mesageType))
            {
                mapByMessage[mesageType].Remove(receiver);
            }
        }

        public void UnregisterAll(object receiver)
        {
            if (!mapByReceiver.TryGetValue(receiver, out var messages))
            {
                return;
            }

            foreach (Type messageType in messages.Keys)
            {
                mapByMessage[messageType].Remove(receiver);
            }

            mapByReceiver.Remove(receiver);
        }

        private void SendAsInterfaces<TMessage>(TMessage message, Type messageType)
            where TMessage : IMessage
        {
            foreach (Type interfaceType in messageType.GetInterfaces().Where(i => baseType.IsAssignableFrom(i)))
            {
                Send(message, interfaceType);
            }
        }

        private void Send<TMessage>(TMessage message, Type messageType)
            where TMessage : IMessage
        {
            if (!mapByMessage.TryGetValue(messageType, out var receivers))
            {
                return;
            }

            logger.Trace($"Message [{typeof(TMessage).Name}] is receiving by {receivers.Count} receiver(s) as [{messageType.Name}].");

            foreach (Delegate handlerDel in receivers.Values)
            {
                Action<TMessage> handler = (Action<TMessage>)handlerDel;
                handler.Invoke(message);
            }
        }
    }
}
