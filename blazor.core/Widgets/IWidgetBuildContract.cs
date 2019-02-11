using Blazor.Core.Messaging;

namespace Blazor.Core.Widgets
{
    public interface IWidgetBuildContract
    {
        void SetPresenter(IWidgetPresenter presenter);

        void SetCustomisation(object constomisation);

        void SetState(object state);

        void SetMessageBus(IMessageBus bus);
    }
}