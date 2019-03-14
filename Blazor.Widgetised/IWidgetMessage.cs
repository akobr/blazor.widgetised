using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised
{
    public interface IWidgetMessage : ISystemMessage
    {
        void ProcessMessage(IWidgetManagementService service);
    }
}
