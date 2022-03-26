using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.Singletons
{
    public class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (singletonDestroyed)
                {
                    Debug.LogWarningFormat($"LazySingleton: {nameof(T)} was already destroyed by quiting game. Returning null");
                    return null;
                }

                if (!instance)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                return instance;
            }
        }

        static bool singletonDestroyed = false;
        static T instance;

        protected virtual void Awake()
        {
            // enforce single instance rule
            if (instance == null && !singletonDestroyed)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance != this) return;

            singletonDestroyed = true;
            instance = null;
        }
    }
}