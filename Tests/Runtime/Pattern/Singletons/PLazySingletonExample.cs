using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gummi.Pattern.Singletons;

namespace Gummi.Tests.Pattern.Singletons
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