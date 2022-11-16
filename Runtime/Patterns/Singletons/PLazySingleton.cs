using UnityEngine;

namespace Gummi.Patterns
{
    public class PLazySingleton<T> : LazySingleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            if (this != Instance) return;

            DontDestroyOnLoad(this);
        }
    }
}