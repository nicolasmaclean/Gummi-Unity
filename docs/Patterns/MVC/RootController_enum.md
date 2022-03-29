# class abstract RootController\<enum\>

---

namespace: Gummi.Pattern.MVC

Inherits: MonoBehaviour

---

A base class for a root controller of an app with `enum` being the possible states the app may be in.

`protected abstract SubController<T> GetController(T state)` must be implemented for `RootController` methods to access any `SubController`s corresponding to a value of `enum`. The example `AppController` utilizes serialized fields in Unity to store references to all `SubControllers` and connects `enum` values to the appropriate field.

A scene in Unity should probably have only one `RootController` instances.
