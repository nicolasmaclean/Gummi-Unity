# class abstract SubController\<enum\>

namespace: [Gummi.Pattern.MVC](./MVC.md)  
Inherits: `MonoBehaviour`

## Summary

A base class for a bare-bones SubController, derived classes should handle their connection to any/all visuals by showing/hiding them as the `SubController` is enabled/disabled.

SubController's should connect input processing logic to its view and manipulate the application's model's data.

## Fields

`public RootController<TAppState> root`: The `RootController` this instance is attached to. Useful for updating the applications state.

## Methods

`public virtual void Engage()`: enables `this.gameObject`.

`public virtual void Disengage()`: disables `this.gameObject`.
