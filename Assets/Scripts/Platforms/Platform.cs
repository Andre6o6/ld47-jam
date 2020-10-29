using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool stayAfterFrame;
    public bool wasBeforeFrame;

    protected Animator anim;

    protected virtual void OnEnable()
    {
        anim = GetComponent<Animator>();

        if (!wasBeforeFrame)
        {
            anim.SetTrigger("Create");
        }
    }

    public void Disappear()
    {
        if (!stayAfterFrame)
        {
            anim.SetTrigger("Destroy");
        }
    }
}