using System;
using System.Collections.Generic;

namespace Blazor.PureMvc.Messaging
{
    public class MessageBus : IMessageBus
    {
        private readonly IDictionary<Type, IDictionary<object, Delegate>> mapByMessage;
        private readonly IDictionary<object, IDictionary<Type, Delegate>> mapByReceiver;

        public MessageBus()
        {
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
            if (!mapByMessage.TryGetValue(typeof(TMessage), out var receivers))
            {
                return;
            }

            foreach (Delegate handlerDel in receivers.Values)
            {
                Action<TMessage> handler = (Action<TMessage>)handlerDel;
                handler.Invoke(message);
            }
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
    }
}
