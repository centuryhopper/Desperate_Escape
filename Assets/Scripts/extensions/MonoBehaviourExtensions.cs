using UnityEngine;
using System;
using System.Collections;

public static class MonoBehaviourExtensions
{
    public static void CallWithDelay(this MonoBehaviour mono, Action method, float delay)
    {
        mono.StartCoroutine(CallWithDelayRoutine(method, delay));
    }

    public static void CallWithRepeatingDelay(this MonoBehaviour mono, Action method, float delay)
    {
        mono.StartCoroutine(CallWithDelayRepeatingRoutine(method, delay));
    }

    static IEnumerator CallWithDelayRoutine(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }

    static IEnumerator CallWithDelayRepeatingRoutine(Action method, float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            method();
        }
    }
}