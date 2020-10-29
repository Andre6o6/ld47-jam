using UnityEngine;
using UnityEngine.Events;

public class Frame : MonoBehaviour
{
    public float cameraSize = 9;
    public Vector2 cameraPosition;

    public bool updateBasePosition;
    public Vector2 spawnPosition;
    public Vector2 gravityPosition;

    public UnityEvent onRestart;
    public UnityEvent onNextFrame;

    private void OnEnable()
    {
        FrameManager.instance.currentFrame = this;

        if (FrameManager.instance.frameCamera != null)
        {
            FrameManager.instance.frameCamera.SetCamera(cameraPosition, cameraSize);
        }
    }

    public Vector2 GetSpawnGravity()
    {
        Vector2 gravity = gravityPosition - spawnPosition;
        if (Mathf.Abs(gravity.x) > Mathf.Abs(gravity.y))
        {
            gravity.x = Mathf.Sign(gravity.x);
            gravity.y = 0;
        }
        else
        {
            gravity.x = 0;
            gravity.y = Mathf.Sign(gravity.y);
        }
        
        return gravity;
    }

    private void OnDrawGizmos()
    {
        Vector2 size = new Vector2()
        {
            x = 2 * cameraSize * Camera.main.aspect,
            y = 2 *cameraSize
        };
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraPosition, size);
    }
}
