using System.Collections.Generic;

namespace Blazor.PureMvc.Widgets
{
    public class WidgetsProxy
    {
        private readonly Dictionary<string, IMediator> map;

        public WidgetsProxy()
        {
            map = new Dictionary<string, IMediator>();
        }

        public IMediator Get(string widgetKey)
        {
            map.TryGetValue(widgetKey, out IMediator widget);
            return widget;
        }

        public void Register(IMediator mediator)
        {
            map[mediator.Key] = mediator;
        }

        public void Unregister(string widgetKey)
        {
            map.Remove(widgetKey);
        }
    }
}
