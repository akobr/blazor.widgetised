using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Blazor.Widgetised.Logging
{
    public class TextLogger : ILogger
    {
        private const int EXCEPTION_MAX_NESTING_LEVEL = 3;

        private readonly IWritable target;

        public TextLogger(IWritable target)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            target.Write($"{level}: ");
            target.WriteLine(message);
            WriteObjects(args);
        }

        [Conditional("DEBUG")]
        private void WriteObjects(IEnumerable<object>? args)
        {
            if (args == null)
            {
                return;
            }

            foreach (object objectToWrite in args)
            {
                WriteObject(objectToWrite);
            }
        }


        private void WriteObject(object? objectToWrite)
        {
            if (objectToWrite == null)
            {
                return;
            }

            target.WriteLine(objectToWrite.ToString());

            if (objectToWrite is Exception exception)
            {
                target.WriteLine("EXCEPTION DETAILS:");
                WriteException(exception, 0);
            }
        }

        private void WriteException(Exception exception, int nestingLevel)
        {
            if (exception == null
                || nestingLevel > EXCEPTION_MAX_NESTING_LEVEL)
            {
                return;
            }

            Console.WriteLine($"MESSAGE: {exception.Message ?? string.Empty}");
            Console.WriteLine($"SOURCE: {exception.Source ?? string.Empty}");
            Console.WriteLine(exception.StackTrace ?? string.Empty);

            WriteExceptionData(exception);

            if (exception.InnerException == null)
            {
                return;
            }

            int nextNestingLevel = nestingLevel + 1;
            Console.WriteLine($"INNER EXCEPTION LEVEL {nextNestingLevel}:");
            WriteException(exception.InnerException, nextNestingLevel);
        }

        private void WriteExceptionData(Exception exception)
        {
            if (exception?.Data == null
                || exception.Data.Count < 1)
            {
                return;
            }

            StringBuilder stb = new StringBuilder();

            foreach (DictionaryEntry entry in exception.Data)
            {
                stb.Append(entry.Key ?? "[unknown]");
                stb.Append(" = ");
                stb.Append(entry.Value ?? "[null]");
                stb.Append("; ");
            }

            if (stb.Length < 2)
            {
                return;
            }

            stb.Remove(stb.Length - 2, 2);
            target.WriteLine("EXCEPTION DATA:");
            target.WriteLine(stb.ToString());
        }

    }
}
