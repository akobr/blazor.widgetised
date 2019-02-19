using Blazor.Core.Interactions;
using System;

namespace Blazor.Core.Widgets
{
    public class PresenterInContainerActivationContext : IPresenterInContainerActivationContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public string ContainerKey { get; set; }

        public Action ContinueWith { get; set; }
    }
}
