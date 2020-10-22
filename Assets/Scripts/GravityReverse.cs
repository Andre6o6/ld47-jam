using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverse : MonoBehaviour
{
    public float resetTime;
    private Collider2D col;
    private Animator anim;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var controller = collision.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.SetGravity(-controller.gravity, true);
            col.enabled = false;
            anim.SetBool("Enabled", false);
            StartCoroutine(gameObject.DoAfterTime(resetTime, () => {
                col.enabled = true; anim.SetBool("Enabled", true);
            }));
        }
    }
}
