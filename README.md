# Widgetised Blazor (blazor.widgetised)

Library with the support of widgets for a Blazor application.
The main goal is to help with loose coupling inside a system.
Initial idea cames from [PureMVC](http://puremvc.org/) architecture, which has been simplified and polish.

> Preview alfa release is coming soon.

![Library preview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/preview.gif)

## Road map

- [ ] Create architecture overview diagram
- [ ] Better logging (more trace logs)
- [ ] Release alfa version
- [ ] Unit tests
- [ ] Write decent documentation
- [ ] Create more complex example application
- [ ] Implement generic layout widget

## Architecture

* **Widget**: integrated unit, mediator and razor component with or without view model.
* **MessageBus**: centralised message bus for widgets and services.
* **Interaction**: communication (message) from platform (UI) to logic layer (mediator).

![architecture overview](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/architecture.png)

## Messaging (loose coupling)

* **platform -> logic**: bubling of interactions in component tree which ends in meditor. 
* **logic <-> logic**: broadcast messaging bus, between services and mediators.

![messaging](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/messaging.png)

## Components

The library contains a couple of predefined components.

### Widget

**Place a widget inline.**

![widget component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-widget.png)

Registered widget can be placed just with `VariantName` if state should be automatically preserved a `Position` need to be specified, as well. 
A totally custom widget can be rendered with `Description` property.

### Container

**A place holder for dynamic content.** A content can be any `RenderFragment` but predominantly intended for widgets.

![container component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-container.png)

### ViewModelRegion

Simple region component to define a part of UI which use MVVM patern.

![mvvm region component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-vm-region.png)

### SystemComponent (abstract)

Abstract **base class for custom component** with support of interaction pipeline and MVVM pattern.

![system base component](https://raw.githubusercontent.com/akobr/blazor.widgetised/master/docs/diagrams/component-system.png)