# LazySingleton\<T\>

---

namespace: Gummi.Pattern.Singletons

Inherits: MonoBehaviour

---

`LazySingleton` shares the same functionality as the `Singleton` base class, but provides a lazy-construction of `T.Instance`. If there is no instance of `T` in the scene when `T.Instance` is accessed, an empty game object will be created an have `T` added to it. This game object becomes `T.Instance`.
