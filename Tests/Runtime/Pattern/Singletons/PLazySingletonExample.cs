using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Patterns.Singletons;

namespace Gummi.Tests.Patterns.Singletons
{
    public class PLazySingletonExample : PLazySingleton<PLazySingletonExample>
    {
        int _accessCount = 0;

        public int Access()
        {
            return _accessCount++;
        }
    }
}