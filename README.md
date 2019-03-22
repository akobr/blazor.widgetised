# Widgetised Blazor

Library with the support of **widgets** for a Blazor application.
The main goal is to help with loose coupling inside a system.
Initial idea cames from [PureMVC](http://puremvc.org/) architecture, which has been simplified and evolved a little bit.

> Preview alpha release is coming soon. Currently on **Blazor 0.9.0** *preview3-19154-02*. [Visual Studio 2019](https://visualstudio.microsoft.com/vs/preview/) *(Preview 4 or later)* with the ASP.NET and web development workload selected is needed same like [.NET Core 3.0 Preview 3 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0) (3.0.100-preview3-010431).

![Library preview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/preview.gif)

## Concepts

* **Widget**: an independent and self-containing unit of mediator and presenter (razor components).
* **MessageBus**: centralised message bus for widgets and services. Powerful tool to decoupled items in the system, must be used wisely.
* **Interaction**: communication from platform (component) to logic layer (mediator).

### Supporting concepts

* **state**: an automatically stored and restored state of a widget, when the same widget is placed to the same position.
* **variant**: a predefined widget configuration; simplifies a widget creation.
* **container**: named placeholder in UI where can be placed content, dynamically.
* **customisation**: allows configuring a currently created widget.
* * **store**: a piece of the application state.

![architecture overview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/architecture.png)

## Messaging (loose coupling)

* **platform -> logic**: bubbling of interactions in a component tree which ends in a mediator.
* **logic <-> logic**: a broadcast messaging bus, between services and mediators.

![messaging](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/messaging.png)

## Components

The library contains a couple of predefined components.

### Widget (inline)

**Place a widget inline.**

![widget component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-widget.png)

A registered widget can be placed just with `VariantName` if a state should be automatically preserved a `Position` need to be specified, as well. 
A totally custom widget can be rendered with `Description` property.

Widget registration should happen by `IWidgetFactory` interface:

```csharp
public static void RegisterWidgetVariants(this IComponentsApplicationBuilder appBuilder)
{
  IWidgetFactory widgetFactory = appBuilder.Services.GetService<IWidgetFactory>();
  widgetFactory.Register("MyWidgetVariant", new WidgetVariant(typeof(MyWidgetMediator)));
}
```

To place an inline widget inside Razor component can be done by `Widget` element:

```cshtml
<Widget VariantName="MyWidgetVariant" /> 
```

If you planning to dynamically instantiate a widget from a code-based component you can render it directly by `RenderTreeBuilder`.

```csharp
builder.OpenComponent<Widget>(0);
builder.AddAttribute(1, nameof(Widget.VariantName), "MyWidgetVariant");

// or specify the exact description of the new widget
// builder.AddAttribute(
//  1,
//  nameof(Widget.Description), 
//  new WidgetDescription()
//  {
//    Variant = new WidgetVariant(typeof(MyWidgetMediator))
//    ...
//  });

builder.CloseComponent();
```

### Container

**A place holder for dynamic content.** Content can be any `RenderFragment` but predominantly intended for widgets.

![container component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-container.png)

Placement of a container inside a component:

```cshtml
<Container Key="MyContainer">
  This content will be used when the container is <strong>empty</strong>.
</Container>
```

To instantiate a widget inside the container from any place of code can be done by direct access to service `IWidgetManagementService`:

```csharp
private IWidgetManagementService Service { get; }
private IMessageBus MessageBus { get; }

void Foo()
{
  // Build a widget instance through service
  WidgetInfo info = Service.Build("MyWidgetVariant");

  // Activate the widget into the container
  Service.Activate(info.Id, "MyContainer");

  // Build and activate the widget in one hit as a start message
  MessageBus.Send(new StartWidgetMessage()
  {
    VariantName = "MyWidgetVariant",
    Position = "MyContainer"
  });
}
```

Or by sending a message `StartWidgetMessage` to `IMessageBus`:

```csharp
readonly IMessageBus messageBus;

void StartWidget()
{
  messageBus.Send(new StartWidgetMessage 
  {
    VariantName = "MyWidgetVariant",
    Position = "MyContainer"
  });
}
```

### ViewModelRegion

Simple region component to define a part of UI which use MVVM pattern.

![mvvm region component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-vm-region.png)

Any view model which implements `INotifyPropertyChanged` can be used as a parameter for the region component:

```csharp
public class MyViewModel : INotifyPropertyChanged
{
  private string text;
  private bool flag;
  private IImmutableList<string> items;

  public event PropertyChangedEventHandler PropertyChanged;

  public string Text
  {
    get => text;
    set => Set(ref text, value);
  }

  public bool Flag
  {
    get => flag;
    set => Set(ref flag, value);
  }

  public IImmutableList<string> Items
  {
    get => items;
    set => Set(ref items, value);
  }

  private void Set<TValue>(ref TValue field, TValue value, [CallerMemberName] string  memberName = "")
  {
    field = value;
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
  }
}
```

The minimalistic way is to pass a view model and define content for the region component.

```cshtml
<ViewModelRegion ViewModel="@ViewModel">
  <p>
    Text: <i>@ViewModel.Text</i><br />
    Flag: <i>@ViewModel.Flag</i>
  </p>
</ViewModelRegion>

@functions
{
  private MyViewModel ViewModel { get; } = new MyViewModel();
}
```

To specify on which properties the region is going to be updated use `Filter` parameter. Any *regular expression* is allowed.

```cshtml
<ViewModelRegion ViewModel="@ViewModel" Filter="(Items?|Child(ren)?)">
  <ul>
    @foreach (string item in ViewModel.Items)
    {
      <li>@item</li>
    }
  </ul>
</ViewModelRegion>
```

### SystemComponent (abstract)

Abstract **base class for custom component** with the support of interaction pipeline and MVVM pattern.

![system base component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-system.png)

A system component can be used with any view model. When the model implements `INotifyPropertyChanged` will be automatically updated on any property change.

```cshtml
@using Blazor.Widgetised.Components
@using Blazor.Widgetised.Messaging

@inherits SystemComponent<MyViewModel>

<table>
  <tr>
    <td>Text</td>
    <td>
      <input type="text" id="text" bind="@ViewModel.Text" />
    </td>
  </tr>
  <tr>
    <td>Flag</td>
    <td>
      <label><input type="checkbox" id="flag" bind="@ViewModel.Flag" />True</label>
    </td>
  </tr>
  <tr>
    <td>Controls</td>
    <td>
      <button onclick="@OnButtonClick">Add item</button>
    </td>
  </tr>
</table>

@functions
{
  private void OnButtonClick()
  {
    InteractionPipe.Send<Messages.Click>(new Messages.Click
    {
      Name = "AddItem",
      Content = "next item"
    });
  }
}
```

## Customisation of widget

To be able to create more flexible and configurable widgets: A `WidgetVariant` or `WidgetDescription` can contain a custom object which will be used as the static or dynamic configuration for a widget.

The first step is to create strongly typed customisation `IExampleCustomisation`:

```csharp
public interface IExampleCustomisation
{
  string Text { get; set; }

  int Number { get; set; }

  bool Flag { get; set; }
}
```

Create your own implementation of the customisation contract. Just implement the interface created in the previous step.

```csharp
public class ExampleCustomisation : IExampleCustomisation
{
  public ExampleCustomisation()
  {
    // Default values
    Text = "default";
    Number = 42;
    Flag = true;
  }

  public string Text { get; set; }

  public int Number { get; set; }

  public bool Flag { get; set; }
}
```

The customisation can be overwritten by specifying new instance inside `WidgetDescription` if you need to change only a specific set of properties a dynamic customisation object `Customisation<TCustomisation>` should be used.

```csharp
dynamic customisation = new Customisation<IExampleCustomisation>();

// only the specific subset of properties can be overwritten
customisation.Number = 42;
customisation.Flag = false;

WidgetDescription description = new WidgetDescription()
{
  VariantName = "MyWidetVariant",
  Customisation = customisation
};

// when the widget's going to be created the original customisation from the variant is merged with the dynamic changes
```

## Road map

### Phase 1

- [X] Better logging (more trace logs)
- [X] Use nullable reference types (C# 8.0)
- [X] Create MVVM example
- [X] Write decent documentation
- [ ] Create architecture overview diagram
- [ ] Unit tests
- [ ] Release alfa version

### Phase 2

- [ ] Add widget lifetime manager
- [ ] Implement generic layout widget
- [ ] Experiment with ReactiveUI