using Blazor.PureMvc.Messaging;

namespace Blazor.PureMvc.Interactions
{
    public interface IInteractionPipeline
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;
    }
}
