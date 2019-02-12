using Blazor.Core.Interactions;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.Core.Widgets
{
    public class PresenterActivateInlineContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public Action<RenderFragment> RenderAction { get; set; }

        public Action ContinueWith { get; set; }
    }
}
