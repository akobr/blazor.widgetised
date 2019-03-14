namespace Blazor.Widgetised.Logging
{
    public interface IWritable
    {
        void Write(string text);

        void WriteLine(string text);
    }
}
