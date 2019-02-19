using System;

namespace Blazor.Widgetised
{
    public class WidgetIdentifier : IWidgetIdentifier
    {
        private const string UNKNOWN_KEY = "UNKNOWN";
        private const string UNKNOWN_POSITION = "ANYWHERE";
        private const char SEPARATOR = '|';

        public WidgetIdentifier(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public string Name { get; set; }

        public string Position { get; set; }

        public string GetKey()
        {
            throw new NotImplementedException();
        }
    }
}
