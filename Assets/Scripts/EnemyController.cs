using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour
{
    private CharacterController controller;
    private CharacterController target;
    public LayerMask obstacleMask;

    public float movementSpeed;
    public bool lockedIntoMovement;

    private Animator anim;
    private float height;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        target = FindObjectOfType<PlayerInput>().GetComponent<CharacterController>();

        height = GetComponent<Collider2D>().bounds.extents.y - 0.05f;
    }

    private void OnEnable()
    {
        controller.enabled = true;
        controller.ResetCharacter();
        GetComponent<EnemyLeap>().enabled = true;
    }

    private void Update()
    {
        if (target == null || !target.enabled || Cristal.cristalCount == 0)
        {
            anim?.SetBool("Die", true);
            controller.canBeControlled = false;
            controller.enabled = false;
            GetComponent<EnemyLeap>().enabled = false;
            return;
        }

        if (!controller.canBeControlled)
            return;
        
        Vector2 origin = (Vector2)transform.position - controller.platformNormal * height;
        Vector2 direction = (Vector2)target.transform.position - origin;

        Debug.DrawRay(origin, direction);

        var hit = Physics2D.Raycast(origin, direction.normalized, direction.magnitude, obstacleMask);
        if (hit && hit.rigidbody.gameObject == controller.platform)
        {
            if (!lockedIntoMovement || controller.localVelocity.x == 0)
            {
                //int sign = (target.platform == platform && Vector2.Dot(target.platformNormal, platformNormal) == -1) ? -1 : 1;
                int sign = 1;

                if (Vector2.SignedAngle(-controller.platformNormal, direction) > 0)
                {
                    controller.localVelocity.x = sign * movementSpeed;
                }
                else
                {
                    controller.localVelocity.x = -sign * movementSpeed;
                }

                lockedIntoMovement = true;
                StartCoroutine(UnlockMovement());
            }
        }
        else if (!hit && Mathf.Abs(Vector2.SignedAngle(controller.platformNormal, direction)) > 60)
        {
            if (Vector2.SignedAngle(-controller.platformNormal, direction) > 0)
            {
                controller.localVelocity.x = movementSpeed;
            }
            else
            {
                controller.localVelocity.x = -movementSpeed;
            }
        }
        else
        {
            if (controller.canBeControlled && controller.grounded)
                GetComponent<EnemyLeap>().Leap(target.transform);

            controller.localVelocity.x = 0;
        }

        anim.SetFloat("vX", Mathf.Abs(controller.localVelocity.x));
        anim.SetFloat("vY", controller.localVelocity.y);
        anim.SetBool("grounded", controller.grounded);
    }

    private IEnumerator UnlockMovement()
    {
        yield return new WaitForSeconds(2);
        lockedIntoMovement = false;
    }

    public void Hide()
    {
        anim?.SetBool("Die", false);
        this.gameObject.SetActive(false);
    }
}
