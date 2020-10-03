using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravityForce;
    public float movementSpeed;

    private ContactPoint2D[] contactPoints = new ContactPoint2D[16];

    private Vector2 platformNormal;

    private bool grounded;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.down * gravityForce;
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            Physics2D.gravity = -platformNormal * gravityForce;

            var h = Input.GetAxis("Horizontal");
            var tangent = new Vector2(platformNormal.y, -platformNormal.x);
            rigid.MovePosition(rigid.position + h * tangent * movementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            int count = collision.GetContacts(contactPoints);
            for (int i = 0; i < count; i++)
            {
                var newNormal = collision.GetContact(i).normal;

                //TODO check grounded and stuff
                if (platformNormal == Vector2.zero ||
                    Vector2.Dot(platformNormal, newNormal) > 0.9f)  //FIXME
                {
                    Debug.DrawRay(collision.GetContact(i).point, newNormal, Color.red);

                    platformNormal = newNormal;
                    break;
                }
            }
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            grounded = false;
        }
    }
}
