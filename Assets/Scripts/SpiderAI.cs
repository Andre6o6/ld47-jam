using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SpiderAI : MonoBehaviour
{
    private CharacterController controller;
    private CharacterController target;

    public float movementSpeed;
    public float movementLockTime = 2;
    private bool lockedIntoMovement;

    public float leapForce;
    public float waitTime;
    private GameObject lastPlatform;

    private Animator anim;
    private float height;

    private bool canLeap = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        target = FindObjectOfType<PlayerInput>().GetComponent<CharacterController>();   //FIXME

        height = GetComponent<Collider2D>().bounds.extents.y - 0.05f;
    }

    private void OnEnable()
    {
        controller.enabled = true;
        controller.ResetCharacter();
        controller.gravityScale = 0;
    }

    private void Update()
    {
        if (target == null || !target.enabled || Cristal.cristalCount == 0)
        {
            anim?.SetBool("Die", true);
            controller.canBeControlled = false;
            controller.enabled = false;
            return;
        }

        if (controller.canBeControlled && controller.grounded)
        {
            Vector2 origin = (Vector2)transform.position - controller.platformNormal * height;
            Vector2 ray = (Vector2)target.transform.position - origin;

            Debug.DrawRay(origin, ray);

            var hit = Physics2D.Raycast(origin, ray.normalized, ray.magnitude, controller.platformMask);
            if (hit && hit.rigidbody.gameObject == controller.platform ||
                !hit && Mathf.Abs(Vector2.SignedAngle(controller.platformNormal, ray)) > 60)
            {
                if (!lockedIntoMovement || controller.localVelocity.x == 0)
                {
                    float sign = Mathf.Sign(Vector2.SignedAngle(-controller.platformNormal, ray));

                    controller.localVelocity.x = sign * movementSpeed;

                    lockedIntoMovement = true;
                    StartCoroutine(UnlockMovement());
                }
            }
            else    //!hit
            {
                if (canLeap)
                {
                    canLeap = false;

                    controller.localVelocity.x = 0;
                    StartCoroutine(Leap(target.transform));
                }
            }
        }

        anim.SetBool("grounded", controller.grounded);
        anim.SetFloat("vX", Mathf.Abs(controller.localVelocity.x));
        anim.SetFloat("vY", controller.localVelocity.y);
    }

    private IEnumerator Leap(Transform target)
    {
        //Float
        controller.canBeControlled = false;
        controller.grounded = false;
        controller.gravityScale = 0;
        lastPlatform = controller.platform;

        controller.transform.Translate(controller.platformNormal * 0.5f);
        anim.SetBool("float", true);

        yield return new WaitForSeconds(waitTime);

        //Leap
        Vector2 direction = target.position - transform.position;
        controller.platformNormal = -direction.normalized;
        controller.localVelocity = Vector2.down * leapForce;
        anim.SetBool("float", false);
    }

    private IEnumerator UnlockMovement()
    {
        yield return new WaitForSeconds(movementLockTime);
        lockedIntoMovement = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canLeap)   //if leaped
        {
            controller.canBeControlled = true;
            controller.gravityScale = 1;
            controller.localVelocity = Vector2.zero;
            //controller.SetVelocity(Vector2.zero);

            StartCoroutine(gameObject.DoAfterTime(0.5f, () => { canLeap = true; }));
        }
    }

    public void EnableGravity()
    {
        controller.gravityScale = 1;
        SetGravityDirection();
    }

    public void SetGravityDirection()
    {
        Vector2[] dirs = new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right };
        foreach (var direction in dirs)
        {
            if (Physics2D.Raycast(controller.transform.position, direction, 1, controller.platformMask))
            {
                controller.SetGravity(direction);
            }
        }
    }

    //FIXME hz why I added that
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (!controller.canBeControlled)
        {
            if (collision.gameObject != lastPlatform)
            {
                controller.canBeControlled = true;
                controller.ResetCharacter();
            }
        }
    }*/
}
