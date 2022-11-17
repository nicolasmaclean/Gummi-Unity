using UnityEngine;

namespace Gummi.Patterns
{
    public class PSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            if (this != Instance) return;

            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
    }
}
