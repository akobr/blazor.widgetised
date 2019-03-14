using System;

namespace Blazor.Widgetised.Logging
{
    public class WritableConsole : IWritable
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
