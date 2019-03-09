# Widgetised Blazor (blazor.widgetised)

Library with the support of widgets for a Blazor application.
The main goal is to help with loose coupling inside a system.
Initial idea cames from [PureMVC](http://puremvc.org/) architecture, which has been simplified and polish.

> Preview alpha release is coming soon. Currently on **Blazor 0.9.0** *preview3-19154-02*. [Visual Studio 2019](https://visualstudio.microsoft.com/vs/preview/) *(Preview 4 or later)* with the ASP.NET and web development workload selected needed same like [.NET Core 3.0 Preview 3 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0) (3.0.100-preview3-010431).

![Library preview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/preview.gif)

## Road map

- [ ] Create architecture overview diagram
- [ ] Better logging (more trace logs)
- [ ] Release alfa version
- [ ] Unit tests
- [ ] Write decent documentation
- [ ] Add widget lifetime manager
- [ ] Create more complex example application
- [ ] Implement generic layout widget
- [ ] Use nullable reference types (C# 8.0)
- [ ] Experiment with ReactiveUI

## Architecture

* **Widget**: integrated unit, mediator and razor component with or without view model (MVVM).
* **MessageBus**: centralised message bus for widgets and services.
* **Interaction**: communication (message) from platform (UI) to logic layer (mediator).

![architecture overview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/architecture.png)

## Messaging (loose coupling)

* **platform -> logic**: bubling of interactions in component tree which ends in mediator. 
* **logic <-> logic**: broadcast messaging bus, between services and mediators.

![messaging](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/messaging.png)

## Components

The library contains a couple of predefined components.

### Widget (inline)

**Place a widget inline.**

![widget component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-widget.png)

Registered widget can be placed just with `VariantName` if state should be automatically preserved a `Position` need to be specified, as well. 
A totally custom widget can be rendered with `Description` property.

Widget registration should happen by `IWidgetFactory` interface:

```csharp
 public static void RegisterWidgetVariants(this IComponentsApplicationBuilder appBuilder)
{
    IWidgetFactory widgetFactory = appBuilder.Services.GetService<IWidgetFactory>();

    widgetFactory.Register("MyWidgetVariant", new WidgetVariant
    {
        MediatorType = typeof(MyWidgetMediator)
    });
}
```

To place inline widget inside Razor component can by done by `Widget` element:

```cshtml
<Widget VariantName="@WidgetVariants.SHOW_WIDGET" /> 
```

If you planning to dynamically instantiate widget from a code-based component you can render it directly by `RenderTreeBuilder`.

```csharp
builder.OpenComponent<Widget>(0);
builder.AddAttribute(1, nameof(Widget.VariantName), "MyWidgetVariant");

// or specify the exact description of the new widget
//builder.AddAttribute(1, nameof(Widget.Description), 
//    new WidgetDescription()
//    {
//        Variant = new WidgetVariant()
//        {
//            MediatorType = typeof(MyWidgetMediator)
//        }
//        ...
//    });

builder.CloseComponent();
```

### Container

**A place holder for dynamic content.** A content can be any `RenderFragment` but predominantly intended for widgets.

![container component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-container.png)

Placement of a container inside a component:

```cshtml
<Container Key="MyContainer">
    This content will be used when the container is <strong>empty</strong>.
</Container>
```

To instanciate a widget inside the container from any place of code can be done by direct access to service `IWidgetManagementService` or by sending a message `Widget.Start` to `IMessageBus`.

```csharp
private IWidgetManagementService Service { get; }
private IMessageBus MessageBus { get; }

void Foo()
{
    // Build a widget instance through service
    WidgetInfo info = Service.Build("MyWidgetVariant");
    // Activate the widget from the container
    Service.Activate(info.Id, "MyContainer");

    // Build and activate the widget in one hit as a start message
    MessageBus.Send(new WidgetMessage.Start()
    {
        VariantName = "MyWidgetVariant",
        Position = "MyContainer"
    });
}
```

### ViewModelRegion

Simple region component to define a part of UI which use MVVM patern.

![mvvm region component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-vm-region.png)

### SystemComponent (abstract)

Abstract **base class for custom component** with support of interaction pipeline and MVVM pattern.

![system base component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-system.png)