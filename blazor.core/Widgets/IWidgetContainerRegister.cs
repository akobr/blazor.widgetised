using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public interface IWidgetContainerRegister
    {
        void Register(string key, IContainer container);

        void Unregister(string key);
    }
}
