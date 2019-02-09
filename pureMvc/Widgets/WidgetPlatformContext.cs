using Blazor.PureMvc.Interactions;

namespace Blazor.PureMvc.Widgets
{
    public class WidgetPlatformContext
    {
        public IInteractionPipe InteractionPipe { get; set; }

        public string ContainerKey { get; set; }
    }
}
