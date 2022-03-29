# PLazySingleton\<T\>

---

namespace: Gummi.Pattern.Singletons

Inherits: MonoBehaviour

---

`PLazySingleton` shares the same functionality of both `LazySingleton` and `Singleton`, but will also perform `DontDestroyOnLoad(T.Instance)` upon caching a reference to `T.Instance`.
