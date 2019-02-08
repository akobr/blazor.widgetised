using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Core.Widgets
{
    public interface IWidgetPresenterBuildContract
    {
        void SetContainerProvider(IWidgetContainerProvider containerProvider);
    }
}
