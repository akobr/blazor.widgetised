using System;

namespace Blazor.PureMvc.Messaging
{
    public interface IMessageBus
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void Register<TMessage>(object receiver, Action<TMessage> handler)
            where TMessage : IMessage;

        void Unregister<TMessage>(object receiver);

        void UnregisterAll(object receiver);
    }
}
