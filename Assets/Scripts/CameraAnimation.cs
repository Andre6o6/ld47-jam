using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public float from, to;
    public float time;
    private float t;

    IEnumerator Start()
    {
        if (!NextLvlTransition.restarted)
        {
            while (t < time)
            {
                t += Time.deltaTime;
                Camera.main.orthographicSize = Mathf.Lerp(from, to, t / time);
                yield return null;
            }
        }
    }

}
