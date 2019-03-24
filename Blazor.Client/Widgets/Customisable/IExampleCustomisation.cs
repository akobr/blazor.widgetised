namespace Blazor.Client.Widgets.Customisable
{
    // Read-only customisation model
    public interface IExampleCustomisation
    {
        bool Flag { get; }

        int Number { get; }

        string Text { get; }
    }
}