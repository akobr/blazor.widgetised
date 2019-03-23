using Blazor.Widgetised.Interactions;

namespace Blazor.Widgetised.Components
{
    public interface IInteractionPipelineContract
    {
        void SetParentInteractionPipe(IInteractionPipe? parentPipe);
    }
}
