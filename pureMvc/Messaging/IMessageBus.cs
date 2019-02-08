namespace Blazor.PureMvc.Messaging
{
    public interface IMessageBus
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;
    }
}
