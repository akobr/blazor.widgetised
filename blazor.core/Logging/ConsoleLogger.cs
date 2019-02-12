using System;
using System.Diagnostics;

namespace Blazor.Core.Logging
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
