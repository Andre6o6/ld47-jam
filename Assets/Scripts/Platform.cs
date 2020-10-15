using UnityEngine;

public class Platform : MonoBehaviour
{
    public Animator anim;

    protected virtual void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Create");
    }
}