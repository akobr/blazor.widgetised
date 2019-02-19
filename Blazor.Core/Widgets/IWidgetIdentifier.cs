namespace Blazor.Core.Widgets
{
    public interface IWidgetIdentifier
    {
        /// <summary>
        /// Get a name of the widget, normally a variant or a mediator type name. 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get a name of the place where the widget is rendered.
        /// </summary>
        string Position { get; }

        /// <summary>
        /// Get a full string key of the widget, combines the name and the position. 
        /// </summary>
        string GetKey();
    }
}
