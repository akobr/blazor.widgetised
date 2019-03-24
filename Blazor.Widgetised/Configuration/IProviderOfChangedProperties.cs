using System.Collections.Generic;

namespace Blazor.Widgetised.Configuration
{
    public interface IProviderOfChangedProperties
    {
        IEnumerable<string> ChangedProperties();
    }
}
