using System.Collections.Generic;

namespace Blazor.Widgetised
{
    public interface IPartlyChanged
    {
        IEnumerable<string> ChangedPropertyNames();
    }
}
