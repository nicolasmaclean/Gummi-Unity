using UnityEngine;

namespace Gummi.Patterns
{
    public class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            // enforce single instance rule
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance != this) return;

            instance = null;
        }
    }
}