using System;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Presenters
{
    public class PresenterInlineActivationContext : IPresenterInlineActivationContext
    {
        public PresenterInlineActivationContext(Action<RenderFragment> renderAction, Action continueWith, IInteractionPipe interactionPipe)
        {
            RenderAction = renderAction;
            ContinueWith = continueWith;
            InteractionPipe = interactionPipe;
        }

        public Action<RenderFragment> RenderAction { get; }

        public Action ContinueWith { get; }

        public IInteractionPipe InteractionPipe { get; }
    }
}
