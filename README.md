# Widgetised Blazor (blazor.widgetised)

Library with the support of widgets for a Blazor application.
The main goal is to help with loose coupling inside a system.
Initial idea cames from [PureMVC](http://puremvc.org/) architecture, which has been simplified and polish.

> Preview alfa release is coming soon.

![Library preview][doc/preview.gif]

## Road map

[ ] Create architecture overview diagram
[ ] Better logging (more trace logs)
[ ] Release alfa version
[ ] Unit tests
[ ] Write decent documentation
[ ] Create more complex example application
[ ] Implement generic layout widget

## Architecture

* **Widget**: integrated unit, mediator and razor component with or without view model.
* **MessageBus**: centralised message bus for widgets and services.
* **Interaction**: communication (message) from platform (UI) to logic layer (mediator).