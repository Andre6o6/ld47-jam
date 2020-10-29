using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverse : MonoBehaviour
{
    public enum ReverseDirection { reverse, CW, CCW };
    public ReverseDirection reverseDirection;

    public float deceleration = 2;

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
        if (controller != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))    //FIXME not only player?
        {
            Reverse(controller);
        }
    }

    private void Reverse(CharacterController controller)
    {
        //Change velocity
        if (deceleration == 0)
        {
            controller.SetVelocity(Vector2.zero);
        }
        else
        {
            controller.SetVelocity(controller.velocity / deceleration);
        }

        //Change gravity
        if (reverseDirection == ReverseDirection.reverse)
        {
            controller.SetGravity(-controller.gravity, true);
        }
        else
        {
            int sign = reverseDirection == ReverseDirection.CW ? 1 : -1;

            Vector2 newGravity = new Vector2 {
                x = sign * controller.gravity.y,
                y = -sign * controller.gravity.x
            };
            controller.SetGravity(newGravity, true);
        }

        //Reset
        col.enabled = false;
        anim.SetBool("Enabled", false);
        StartCoroutine(gameObject.DoAfterTime(resetTime, () => {
            col.enabled = true; anim.SetBool("Enabled", true);
        }));
    }

    public void Disappear()
    {
        col.enabled = false;
        anim.SetBool("Enabled", false);

        StopAllCoroutines();
    }
}
