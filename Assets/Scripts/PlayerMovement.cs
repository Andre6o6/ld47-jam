﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravityForce;
    public float movementSpeed;
    
    
    private ContactPoint2D[] contactPoints = new ContactPoint2D[16];

    public Vector2 platformNormal { get; private set; }

    public bool grounded;
    private Rigidbody2D rigid;
    public bool canBeControlled = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.down * gravityForce;
        platformNormal = Vector2.up;

        //Instead of destroying, warp to a start position
        //or throw there with gravity 
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (grounded && canBeControlled)
        {
            Physics2D.gravity = -platformNormal * gravityForce;

            var h = Input.GetAxis("Horizontal");
            var tangent = new Vector2(platformNormal.y, -platformNormal.x);
            if (h != 0)
                MoveLocked(h * tangent * movementSpeed * Time.deltaTime);
        }
    }

    private void MoveLocked(Vector2 movement)
    {
        RaycastHit2D[] raycastHits = new RaycastHit2D[16];
        int count = rigid.Cast(movement, raycastHits, movement.magnitude);

        rigid.MovePosition(rigid.position + movement);
        //transform.Translate(movement);

        //Correct movement on the edges
        if (count == 0 && grounded)
        {
            count = rigid.Cast(Physics2D.gravity, raycastHits, Physics2D.gravity.magnitude * Time.deltaTime);
            if (count > 0)
            {
                if (raycastHits[0].distance < 0.01f)
                    return;

                print(raycastHits[0].distance);

                var newMovement = raycastHits[0].distance * Physics2D.gravity.normalized;
                transform.Translate(newMovement);
            }
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
                if (!grounded || Vector2.Dot(platformNormal, newNormal) > 0.9f)  //FIXME how well normals align
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