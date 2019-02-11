using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.Core.Widgets
{
    public interface IWidgetPresenter : IActivatable<WidgetPlatformContext>
    {
        void Activate(Action<RenderFragment> fragmentAction);
    }
}
