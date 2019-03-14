using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Presenters;

namespace Blazor.Widgetised.Mediators
{
    public interface IWidgetMediatorBuildContract
    {
        void SetPresenter(IWidgetPresenter presenter);

        void SetCustomisation(object customisation);

        void SetState(object state);

        void SetLogger(ILogger logger);
    }
}