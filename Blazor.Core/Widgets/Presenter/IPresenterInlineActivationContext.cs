using System;
using Blazor.Core.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public interface IPresenterInlineActivationContext
    {
        Action ContinueWith { get; }

        IInteractionPipe InteractionPipe { get; }

        Action<RenderFragment> RenderAction { get; }
    }
}