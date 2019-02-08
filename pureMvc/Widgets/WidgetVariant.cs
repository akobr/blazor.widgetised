using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.PureMvc.Widgets
{
    public class WidgetVariant
    {
        public Type MediatorType { get; set; }

        public Type PresenterType { get; set; }

        public Type StateType { get; set; }
    }
}
