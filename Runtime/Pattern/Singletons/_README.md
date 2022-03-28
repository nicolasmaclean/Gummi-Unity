# Singleton Pattern Base Classes

namespace: `Gummi.Pattern.Singletons`

This space includes base classes for commonly used Singleton patterns.

Singletons should used when there must be only 1 instance of a Monobehaviour. This is found commonly in manager scripts for UI, player (single-player games), sound effects, music, etc.

Singletons should not be used when there may/should be more than 1 instance of a Monobehaviour. Some examples include enemies, bullets, etc.

## Why Use Singletons

Singletons allow access to Monobehaviours without requiring a reference to be passed in the form of `Example.Instance`, with `Example` being a derrivative of a Singleton Base class. This is great for enforcing all data/methods around an idea to be directed towards a single instance.

An example could be a SFX system, `SFXManager`, as shown below.

```C#
class SFXManager : Singleton<SFXManager>
{
    AudioSource _source;

    protected override void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    void PlaySFX(AudioClip clip, float volume = 1)
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.Play();
    }
}
```

Any class in the project would be able to play an audio clip using `SFXManager.Instance.PlaySFX(clip)`. This is very handy for prototypes or more simple projects, but is quite limited in its current implementation.

---

# Singleton\<T\>

`T.Instance` is collected by the first `T`'s Awake method, so it requires the developer to create instances of `T` in each scene that requires access to an instance. An example of how to use this is seen in the [Why Use Singletons](#why-use-singletons) section.

# LazySingleton\<T\>

`LazySingleton` shares the same functionality as the `Singleton` base class, but provides a lazy-construction of `T.Instance`. If there is no instance of `T` in the scene when `T.Instance` is accessed, an empty game object will be created an have `T` added to it. This game object becomes `T.Instance`.

# PLazySingleton\<T\>

`PLazySingleton` shares the same functionality of both `LazySingleton` and `Singleton`, but will also perform `DontDestroyOnLoad(T.Instance)` upon caching a reference to `T.Instance`.

---

## Credit

This code is heavily recycled from Patryk Galach's blog post [here](https://www.patrykgalach.com/2019/04/04/singleton-in-unity-love-or-hate/) and his bitbucket repo [here](https://bitbucket.org/gaello/singletons/).

He has not published or mentioned the license for this code in the post or repo, but here is credit.
