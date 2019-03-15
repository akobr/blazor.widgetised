using System;
using System.Collections.Generic;
using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised.Interactions
{
    public class InteractionPipe : IInteractionPipe, IDisposable
    {
        private readonly IDictionary<Type, Delegate> map;
        private IInteractionPipe? parent;

        public InteractionPipe()
        {
            map = new Dictionary<Type, Delegate>();
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);

            if (!map.TryGetValue(messageType, out Delegate hadlerDel))
            {
                Bubble<TMessage>(message);
                return;
            }

            Action<TMessage> handler = (Action<TMessage>)hadlerDel;
            handler.Invoke(message);
        }

        public void Register<TMessage>(Action<TMessage> handler)
            where TMessage : IMessage
        {
            map[typeof(TMessage)] = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void Clear()
        {
            map.Clear();
        }

        public void Dispose()
        {
            Clear();
        }

        public void SetParent(IInteractionPipe? newParent)
        {
            parent = newParent;
        }

        private void Bubble<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            parent?.Send<TMessage>(message);
        }
    }
}
