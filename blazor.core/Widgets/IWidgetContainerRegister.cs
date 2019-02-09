using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerRegister
    {
        void Register(string key, IRenderingContainer container);

        void Unregister(string key);
    }
}
