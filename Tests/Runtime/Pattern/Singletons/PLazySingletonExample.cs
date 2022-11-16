using Gummi.Patterns;

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