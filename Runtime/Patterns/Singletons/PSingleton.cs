using UnityEngine;

namespace Gummi.Patterns.Singletons
{
    public class PSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            if (this != Instance) return;

            DontDestroyOnLoad(this);
        }
    }
}