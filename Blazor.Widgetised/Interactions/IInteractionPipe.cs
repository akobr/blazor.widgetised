using System;
using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised.Interactions
{
    public interface IInteractionPipe
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void Register<TMessage>(Action<TMessage> handler)
            where TMessage : IMessage;

        void Clear();
    }
}
