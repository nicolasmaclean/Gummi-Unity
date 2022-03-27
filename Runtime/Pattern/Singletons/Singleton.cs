using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Pattern.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => _instance;
        static T _instance;

        protected virtual void Awake()
        {
            // enforce single instance rule
            if (_instance == null)
            {
                _instance = this as T;
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