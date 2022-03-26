using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.Singletons
{
    public class PLazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (singletonDestroyed)
                {
                    Debug.LogWarningFormat($"LazyPersistentSingleton: {nameof(T)} was already destroyed by quiting game. Returning null");
                    return null;
                }

                if (!_instance)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                return _instance;
            }
        }

        static bool singletonDestroyed = false;
        static T _instance;

        protected virtual void Awake()
        {
            // enforce single instance rule
            if (_instance == null && !singletonDestroyed)
            {
                _instance = this as T;
                DontDestroyOnLoad(_instance);
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance != this) return;

            singletonDestroyed = true;
            _instance = null;
        }
    }
}