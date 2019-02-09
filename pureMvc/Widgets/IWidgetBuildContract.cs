using Blazor.PureMvc.Messaging;

namespace Blazor.PureMvc.Widgets
{
    public interface IWidgetBuildContract
    {
        void SetPresenter(IWidgetPresenter presenter);

        void SetState(object state);

        void SetMessageBus(IMessageBus bus);
    }
}