using System;
using Blazor.Widgetised.Interactions;

namespace Blazor.Widgetised.Presenters
{
    public interface IPresenterInContainerActivationContext
    {
        string ContainerKey { get; }

        Action ContinueWith { get; }

        IInteractionPipe InteractionPipe { get; }
    }
}