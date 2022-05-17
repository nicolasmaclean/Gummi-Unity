using Gummi.Patterns.Singletons;

namespace Gummi.Tests.Patterns.Singletons
{
    public class SingletonExample : Singleton<SingletonExample>
    {
        int _accessCount = 0;

        public int Access()
        {
            return _accessCount++;
        }
    }
}