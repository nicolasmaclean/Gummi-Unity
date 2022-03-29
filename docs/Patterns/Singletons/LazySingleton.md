# LazySingleton\<T\>

namespace: [Gummi.Pattern.Singletons](./Singletons.md)  
Inherits: `MonoBehaviour`

## Summary

Base class for lazy-loaded, scene-contained Singletons. If the scene does not already contain an instance, it will create one.

## Fields

`public static T Instance`: The instance of `T` in the scene. It will never be null. If there is not an instance in the scene, a new instance will be made/returned.

## Methods

`protected virtual void Awake()`: enforces Singleton pattern.

`protected virtual void OnDestroy()`: sets `T.Instance` to null.
