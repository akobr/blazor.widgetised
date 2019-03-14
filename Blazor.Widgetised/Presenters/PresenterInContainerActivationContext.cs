using System;
using Blazor.Widgetised.Interactions;

namespace Blazor.Widgetised.Presenters
{
    public class PresenterInContainerActivationContext : IPresenterInContainerActivationContext
    {
        public PresenterInContainerActivationContext(string containerKey, Action continueWith, IInteractionPipe interactionPipe)
        {
            ContainerKey = containerKey;
            ContinueWith = continueWith;
            InteractionPipe = interactionPipe;
        }

        public string ContainerKey { get; }

        public Action ContinueWith { get; }

        public IInteractionPipe InteractionPipe { get; }
    }
}
