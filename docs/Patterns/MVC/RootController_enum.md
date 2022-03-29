# class abstract RootController\<enum\>

namespace: [Gummi.Pattern.MVC](./MVC.md)  
Inherits: `MonoBehaviour`

## Summary

A base class for implementing an application's root controller. `enum` contains values for the possible states the application is allowed to be in.

Intented use includes a singlular derived instance of this class in a scene. Though, more than one may be used.

## Fields

`public static T Instance`: The instance of `T` in the scene. It will never be null. If there is not an instance in the scene, a new instance will be made/returned.

## Methods

`protected abstract SubController<enum> GetController(enum state)`: maps possible application states to `SubControllers`.

`public void ChangeController(enum state)`: disengages all SubControllers and engages the SubController mapped to `state`.

`public void DisengageController()`: disengages all SubControllers.
