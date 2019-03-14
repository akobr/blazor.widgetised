namespace Blazor.Widgetised.Logging
{
    public interface ILogger
    {
        void Log(LogLevel level, string message, params object[] args);
    }
}
