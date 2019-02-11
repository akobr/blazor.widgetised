using Blazor.Core.Messaging;
using System;

namespace Blazor.Core.Interactions
{
    public interface IInteractionPipe
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void Register<TMessage>(Action<TMessage> handler)
            where TMessage : IMessage;
    }
}
