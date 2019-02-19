using System;

namespace Blazor.Core.Widgets
{
    public interface IWidgetStateStore
    {
        void Add(string stateKey, object state);

        object Get(string stateKey);

        void Remove(string stateKey);
    }
}