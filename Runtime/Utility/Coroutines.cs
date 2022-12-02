using System;
using System.Collections;
using UnityEngine;

namespace Gummi.Utility
{
    public static class Coroutines
    {
        static Noop b_runner;
        static Noop _runner
        {
            get
            {
                if (b_runner == null)
                {
                    b_runner = Pool.CheckOut<Noop>().GetComponent<Noop>();
                }

                return b_runner;
            }
        }


        public static void Start(IEnumerator coroutine) => _runner.StartCoroutine(coroutine);

        public static IEnumerator WaitThen(float seconds, Action callback, bool realTime = false)
        {
            if (realTime) yield return new WaitForSecondsRealtime(seconds);
            else yield return new WaitForSeconds(seconds);
            
            callback?.Invoke();
        }

        public static IEnumerator WaitTill(Func<bool> predicate, Action callback)
        {
            yield return new WaitUntil(predicate);
            callback?.Invoke();
        }
    }
}