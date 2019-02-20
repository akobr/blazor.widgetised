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
            set => name = string.IsNullOrEmpty(value) ? UNKNOWN_NAME : value;
        }

        public string Position
        {
            get => position;
            set => position = string.IsNullOrEmpty(value) ? UNKNOWN_POSITION : value;
        }

        public string GetKey()
        {
            return $"{Name}|{Position}";
        }
    }
}
