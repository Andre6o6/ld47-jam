using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Gravity
    public float gravityScale = 1;
    public Vector2 gravity { get; private set; }
    public float gravityForce = 20;

    //Movement velocity
    public Vector2 localVelocity;
    public Vector2 velocity { get; private set; }
    private RaycastHit2D[] raycastHits = new RaycastHit2D[16];

    //Snapping to platforms
    public LayerMask platformMask;
    private ContactPoint2D[] contactPoints = new ContactPoint2D[16];
    public GameObject platform;
    public Vector2 platformNormal;
    private Vector2 tangent;

    public bool grounded;
    private Rigidbody2D rigid;
    public bool canBeControlled = true;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gravity = Vector2.down * gravityForce;
        platformNormal = Vector2.up;
    }

    private void Move(Vector2 movement, bool snapped)
    {
        float distance = movement.magnitude;

        int count = rigid.Cast(movement, raycastHits, movement.magnitude);
        /*if (!snapped && count > 0)
        {
            distance = raycastHits[0].distance + 0.01f; //to fire collision
        }*/
        transform.Translate(movement.normalized * distance);

        //Additional movement to correct on edges
        if (snapped)
        {
            if (count == 0 && grounded)     //no collision => no point to update gravity
            {
                //Make a gravity step down
                count = rigid.Cast(Physics2D.gravity, raycastHits, Physics2D.gravity.magnitude * Time.deltaTime);
                if (count > 0)
                {
                    if (raycastHits[0].distance < 0.01f)    //there is some floating point bs
                        return;

                    var newMovement = raycastHits[0].distance * Physics2D.gravity.normalized;
                    transform.Translate(newMovement);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Add gravity
        localVelocity.y -= gravityScale * gravityForce * Time.deltaTime;

        if (canBeControlled)
        {
            //Calc. velocity from local velocity
            //tangent = new Vector2(platformNormal.y, -platformNormal.x);
            //velocity = localVelocity.x * tangent + localVelocity.y * platformNormal;
        }

        //Calc. velocity from local velocity
        tangent = new Vector2(platformNormal.y, -platformNormal.x);
        velocity = localVelocity.x * tangent + localVelocity.y * platformNormal;
        Move(velocity * Time.deltaTime, grounded);

        if (grounded && canBeControlled)
        {
            //Update gravity
            gravity = -platformNormal * gravityForce;

            //Cut gravity if on ground
            localVelocity.y = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (platformMask.Contains(collision.gameObject.layer))
        {
            int count = collision.GetContacts(contactPoints);
            for (int i = 0; i < count; i++)
            {
                var newNormal = collision.GetContact(i).normal;

                if (!grounded || Vector2.Dot(platformNormal, newNormal) > 0.9f)  //FIXME how well normals align
                {
                    Debug.DrawRay(collision.GetContact(i).point, newNormal, Color.red);

                    platformNormal = newNormal;
                    platform = collision.gameObject;
                    break;
                }

                //If walking into wall (=> velocity and normal point in opposite dirs, cos=-1)
                if (grounded && Vector2.Dot(velocity, newNormal) < -0.9f)
                {
                    platformNormal = newNormal;
                    platform = collision.gameObject;

                    tangent = new Vector2(platformNormal.y, -platformNormal.x);
                    velocity = localVelocity.x * tangent + localVelocity.y * platformNormal;
                }
            }
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (platformMask.Contains(collision.gameObject.layer) &&
            platform == collision.gameObject)
        {
            grounded = false;
            platform = null;
        }
    }

    public void SetGravity(Vector2 direction)
    {
        //FIXME mb reset velocity

        platformNormal = -direction.normalized;
        gravity = direction.normalized * gravityForce;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public void ResetCharacter()
    {
        canBeControlled = true;
        gravityScale = 1;

        velocity = Vector2.zero;
        localVelocity = Vector2.zero;

        gravity = Vector2.down * gravityForce;
        platformNormal = Vector2.up;
    }
}
