# class abstract SubController\<enum, uiView\>

namespace: [Gummi.Pattern.MVC](./MVC.md)  
Inherits: [`SubController<enum>`](./SubController_enum.md)

## Summary

A base class for built ontop `SubController<enum>`, `ui` will be shown/hidden as this SubController is engaged/disengaged.

See [SubController\<enum\>](./SubController_enum.md) for more info.

## Fields

`public RootController<TAppState> root`: The `RootController` this instance is attached to. Useful for updating the applications state.

`public uiView UI => ui`

`protected uiView ui`: An instance of `uiView` that is shown/hidden.

## Methods

`public override void Engage()`: enables `this.gameObject` and performs `ui.ShowRoot()`.

`public override void Disengage()`: disables `this.gameObject` and performs `ui.HideRoot()`.
