using System.Collections;
using UnityEngine;

public static class UnityExtensions
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static IEnumerator DoAfterTime(this GameObject obj, float time, System.Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}
