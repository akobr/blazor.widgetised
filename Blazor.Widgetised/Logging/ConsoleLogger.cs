using System;
using System.Diagnostics;

namespace Blazor.Widgetised.Logging
{
    public static class ConsoleLogger
    {
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Console.WriteLine(message);
        }
    }
}
