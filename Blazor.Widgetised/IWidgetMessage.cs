using Blazor.Widgetised.Messaging;

namespace Blazor.Widgetised
{
    internal interface IWidgetMessage : ISystemMessage
    {
        void ProcessMessage(IWidgetManagementService service);
    }
}
