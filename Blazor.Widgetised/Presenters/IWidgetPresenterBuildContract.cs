using Blazor.Widgetised.Logging;

namespace Blazor.Widgetised.Presenters
{
    public interface IWidgetPresenterBuildContract
    {
        void SetWidgetContainerManagement(IWidgetContainerManagement newContainerManagement);

        void SetLogger(ILogger logger);
    }
}
