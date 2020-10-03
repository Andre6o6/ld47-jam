using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();

        if (!NextLvlTransition.restarted)
        {
            anim.SetTrigger("Create");
        }
    }
}