using System;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Presenters
{
    public interface IPresenterInlineActivationContext
    {
        Action ContinueWith { get; }

        IInteractionPipe InteractionPipe { get; }

        Action<RenderFragment> RenderAction { get; }
    }
}