using System.Collections;
using UnityEngine;

public class FrameCamera : MonoBehaviour
{
    public float resizeTime;
    private float t;
    private float currentSize;
    private Vector3 currentPosition;

    [System.Serializable]
    public struct GameobjectsOnEdges
    {
        public GameObject up, down, left, right;

        public bool NotNull()
        {
            return up != null && down != null && left != null && right != null;
        }
    }
    public GameobjectsOnEdges keptOnEdges;
    public float addOffset = 1;

    private void Awake()
    {
        currentSize = Camera.main.orthographicSize;
        currentPosition = Camera.main.transform.position;
    }

    public void SetCamera(Vector2 position, float size)
    {
        if (currentSize == 0)
        {
            currentSize = Camera.main.orthographicSize;
        }

        MoveStuffToEdges(position, size);
        StartCoroutine(CameraAnimation(currentPosition, position, currentSize, size));
    }



    private IEnumerator CameraAnimation(Vector3 fromPos, Vector3 toPos, float fromSize, float toSize)
    {
        //HACK set z
        toPos.z = -10;

        t = 0;
        while (t < resizeTime)
        {
            t += Time.deltaTime;

            if (fromSize != toSize)
                Camera.main.orthographicSize = Mathf.Lerp(fromSize, toSize, t / resizeTime);

            if (fromPos != toPos)
                Camera.main.transform.position = Vector3.Lerp(fromPos, toPos, t / resizeTime);

            yield return null;
        }
        currentSize = toSize;
        currentPosition = toPos;
    }


    private void MoveStuffToEdges(Vector3 position, float size)
    {
        float yOffset = 0.5f * size + addOffset;
        float xOffset = 0.5f * size * Camera.main.aspect + addOffset;

        if (keptOnEdges.NotNull())  //FIXME
        {
            keptOnEdges.up.transform.position = position + Vector3.up * yOffset;
            keptOnEdges.down.transform.position = position + Vector3.down * yOffset;
            keptOnEdges.left.transform.position = position + Vector3.left * xOffset;
            keptOnEdges.right.transform.position = position + Vector3.right * xOffset;
        }
    }
}
