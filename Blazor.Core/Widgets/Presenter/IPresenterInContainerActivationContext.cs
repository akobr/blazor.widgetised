using System;
using Blazor.Core.Interactions;

namespace Blazor.Core.Widgets
{
    public interface IPresenterInContainerActivationContext
    {
        string ContainerKey { get; }

        Action ContinueWith { get; }

        IInteractionPipe InteractionPipe { get; }
    }
}