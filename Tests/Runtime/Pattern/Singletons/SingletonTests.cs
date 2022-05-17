using NUnit.Framework;
using UnityEngine;

namespace Gummi.Tests.Patterns.Singletons
{
    public class SingletonTests : MonoBehaviour
    {
        [Test]
        public void SingletonTest()
        {
            // ensure there is no lazy instance
            Assert.IsNull(SingletonExample.Instance);

            // create instance
            var singleton = new GameObject("SingletonExample");
            singleton.AddComponent<SingletonExample>();

            // attempt to use instance
            Assert.IsNotNull(SingletonExample.Instance);
            Assert.AreEqual(0, SingletonExample.Instance.Access());

            // create more instances
            var originalInstance = SingletonExample.Instance;
            var newInstance = singleton.AddComponent<SingletonExample>();

            Assert.AreNotSame(newInstance,   SingletonExample.Instance);
            Assert.AreSame(originalInstance, SingletonExample.Instance);
        }

        [Test]
        public void LazySingletonTest()
        {
            // ensure there is no lazy instance
            Assert.IsNotNull(LazySingletonExample.Instance);

            // attempt to use instance
            Assert.IsNotNull(LazySingletonExample.Instance);
            Assert.AreEqual(0, LazySingletonExample.Instance.Access());

            // create more instances
            var originalInstance = LazySingletonExample.Instance;
            var newInstance = originalInstance.gameObject.AddComponent<LazySingletonExample>();

            Assert.AreNotSame(newInstance, LazySingletonExample.Instance);
            Assert.AreSame(originalInstance, LazySingletonExample.Instance);
        }

        [Test]
        public void PLazySingletonTest()
        {
            // ensure there is no lazy instance
            Assert.IsNotNull(PLazySingletonExample.Instance);

            // attempt to use instance
            Assert.IsNotNull(PLazySingletonExample.Instance);
            Assert.AreEqual(0, PLazySingletonExample.Instance.Access());

            // create more instances
            var originalInstance = PLazySingletonExample.Instance;
            var newInstance = originalInstance.gameObject.AddComponent<PLazySingletonExample>();

            Assert.AreNotSame(newInstance, PLazySingletonExample.Instance);
            Assert.AreSame(originalInstance, PLazySingletonExample.Instance);
        }
    }
}