using UnityEngine;
using UnityEngine.Events;

public class Frame : MonoBehaviour
{
    public float cameraSize = 9;

    public UnityEvent onRestart;

    private void OnEnable()
    {
        FrameManager.instance.currentFrame = this;

        if (FrameManager.instance.frameCamera != null)
            FrameManager.instance.frameCamera.SetSize(cameraSize);
    }
}
