using Blazor.PureMvc.Interactions;

namespace Blazor.Core.Components
{
    public interface IComponentBuildContract
    {
        void SetInteractionPipe(InteractionPipe pipe);
    }
}
