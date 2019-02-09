using Blazor.PureMvc.Messaging;
using System;

namespace Blazor.PureMvc.Interactions
{
    public interface IInteractionPipe
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void Register<TMessage>(Action<TMessage> handler)
            where TMessage : IMessage;
    }
}
