using Blazor.Core.Interactions;

namespace Blazor.Core.Widgets
{
    public class WidgetPlatformContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public string ContainerKey { get; set; }
    }
}
