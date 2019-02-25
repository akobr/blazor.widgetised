namespace Blazor.Widgetised
{
    public class WidgetIdentifier : IWidgetIdentifier
    {
        private const string UNKNOWN_NAME = "UNKNOWN";
        private const string UNKNOWN_POSITION = "ANYWHERE";

        private string name;
        private string position;

        public WidgetIdentifier(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public string Name
        {
            get => name;
            set => name = ProcessName(value);
        }

        public string Position
        {
            get => position;
            set => position = ProcessPosition(value);
        }

        public string Key => $"{Name}|{Position}";

        public static string BuildKey(string name, string position)
        {
            return $"{ProcessName(name)}|{ProcessPosition(position)}";
        }

        private static string ProcessName(string name)
        {
            return string.IsNullOrEmpty(name) ? UNKNOWN_NAME : name;
        }

        private static string ProcessPosition(string position)
        {
            return string.IsNullOrEmpty(position) ? UNKNOWN_POSITION : position;
        }
    }
}
