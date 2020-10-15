using System.Collections;
using UnityEngine;

public class FrameCamera : MonoBehaviour
{
    public float resizeTime;
    private float t;
    private float currentSize = 5;

    [System.Serializable]
    public struct GameobjectsOnEdges
    {
        public GameObject up, down, left, right;
    }
    public GameobjectsOnEdges keptOnEdges;
    public float addOffset = 1;

    private void Awake()
    {
        currentSize = Camera.main.orthographicSize;
    }

    public void SetSize(float size)
    {
        if (currentSize == size)
            return;

        MoveStuffToEdges(size);
        StartCoroutine(CameraAnimation(currentSize, size));
    }

    private IEnumerator CameraAnimation(float from, float to)
    {
        t = 0;
        while (t < resizeTime)
        {
            t += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(from, to, t / resizeTime);
            yield return null;
        }
        currentSize = to;
    }


    private void MoveStuffToEdges(float size)
    {
        float yOffset = 0.5f * size + addOffset;
        float xOffset = 0.5f * size * Camera.main.aspect + addOffset;

        keptOnEdges.up.transform.position = Vector3.up * yOffset;
        keptOnEdges.down.transform.position = Vector3.down * yOffset;
        keptOnEdges.left.transform.position = Vector3.left * xOffset;
        keptOnEdges.right.transform.position = Vector3.right * xOffset;
    }
}
