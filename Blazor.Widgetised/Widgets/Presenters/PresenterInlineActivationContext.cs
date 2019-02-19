using System;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Presenters
{
    public class PresenterInlineActivationContext : IPresenterInlineActivationContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public Action<RenderFragment> RenderAction { get; set; }

        public Action ContinueWith { get; set; }
    }
}
