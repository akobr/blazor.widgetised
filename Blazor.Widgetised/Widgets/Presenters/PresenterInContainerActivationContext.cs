using System;
using Blazor.Widgetised.Interactions;

namespace Blazor.Widgetised.Presenters
{
    public class PresenterInContainerActivationContext : IPresenterInContainerActivationContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public string ContainerKey { get; set; }

        public Action ContinueWith { get; set; }
    }
}
