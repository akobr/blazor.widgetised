using System.Diagnostics;

namespace Blazor.Widgetised.Logging
{
    public static class LoggerExtensions
    {
        [Conditional("TRACE")]
        public static void Trace(this ILogger? logger, string message, params object[] args)
        {
            logger?.Log(LogLevel.Trace, message, args);
        }

        [Conditional("DEBUG")]
        public static void Debug(this ILogger? logger, string message, params object[] args)
        {
            logger?.Log(LogLevel.Debug, message, args);
        }

        public static void Info(this ILogger? logger, string message, params object[] args)
        {
            logger?.Log(LogLevel.Info, message, args);
        }

        public static void Warning(this ILogger? logger, string message, params object[] args)
        {
            logger?.Log(LogLevel.Warning, message, args);
        }

        public static void Error(this ILogger? logger, string message, params object[] args)
        {
            logger?.Log(LogLevel.Error, message, args);
        }
    }
}
