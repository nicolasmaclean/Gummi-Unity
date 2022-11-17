// This has been taken aarthificals gist on GitHub:
// https://gist.github.com/aarthificial/f2dbb58e4dbafd0a93713a380b9612af
// It was also discussed in their YouTube video: https://www.youtube.com/watch?v=uZmWgQ7cLNI

using System;
using UnityEngine;

namespace Gummi.Utility
{
    /// <summary>
    /// A generic wrapper class for optional variables. This provides a consistent means
    /// of checking if a variable may be used. Expensive null checks are replaced with this.enabled.
    /// </summary>
    /// <remarks>
    /// Requires Unity 2020.1+ to serialize to the inspector.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Optional<T>
    {
        [SerializeField]
        bool enabled;
        
        [SerializeField]
        T value;

        public bool Enabled => enabled;
        public T Value => value;

        public Optional(T initialValue)
        {
            enabled = true;
            value = initialValue;
        }
    }
}