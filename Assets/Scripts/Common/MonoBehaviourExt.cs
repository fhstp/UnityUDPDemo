using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Ac.At.FhStp.UnityUDPDemo
{

    public static class MonoBehaviourExt
    {

        public static Coroutine WaitUntil(this MonoBehaviour mono, Func<bool> pred, TimeSpan timeout)
        {
            IEnumerator Routine()
            {
                var watch = new Stopwatch();
                watch.Start();

                while (!pred() && watch.Elapsed < timeout) yield return null;

                watch.Stop();
            }

            return mono.StartCoroutine(Routine());
        }

    }

}