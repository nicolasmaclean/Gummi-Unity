using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Patterns.Singletons
{
    public class PLazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                return _instance;
            }
        }

        static T _instance;

        protected virtual void Awake()
        {
            // enforce single instance rule
            if (_instance == null)
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

            _instance = null;
        }
    }
}