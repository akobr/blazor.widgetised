using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Blazor.Widgetised.Configuration
{
    public abstract class PartialCustomisationBase : IProviderOfChangedProperties
    {
        private readonly HashSet<string> changedProperties;

        public PartialCustomisationBase()
        {
            changedProperties = new HashSet<string>();
        }

        public IEnumerable<string> ChangedProperties()
        {
            return changedProperties;
        }

        protected void SetProperty<TValue>(ref TValue field, TValue value, [CallerMemberName]string propertyName = "")
        {
            field = value;
            changedProperties.Add(propertyName);
        }
    }
}
