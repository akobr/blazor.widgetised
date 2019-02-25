namespace Blazor.Widgetised
{
    public interface IWidgetIdentifier
    {
        /// <summary>
        /// Get a full string key of the widget, combines the name and the position. 
        /// </summary>
        string Key { get; }
    }
}
