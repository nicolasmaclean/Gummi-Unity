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

## Credit

This code is heavily recycled from Patryk Galach's blog post [here](https://www.patrykgalach.com/2019/04/04/singleton-in-unity-love-or-hate/) and his bitbucket repo [here](https://bitbucket.org/gaello/singletons/).

He has not published or mentioned the license for this code in the post or repo, but here is credit.
