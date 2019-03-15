using Blazor.Widgetised.Interactions;

namespace Blazor.Widgetised.Components
{
    public interface IComponentBuildContract
    {
        void SetParentInteractionPipe(IInteractionPipe? parentPipe);
    }
}
