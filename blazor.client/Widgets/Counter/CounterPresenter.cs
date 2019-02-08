using Blazor.Core.Widgets;
using Blazor.PureMvc;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterPresenter : WidgetPresenter, ICounterPresenter, IInitialisable
    {
        private IComponent component;

        protected override IComponent Component => component;

        public void Initialise()
        {
            // TODO: create component
        }

        public void SetCount(int count)
        {
            throw new NotImplementedException();
        }
    }
}
