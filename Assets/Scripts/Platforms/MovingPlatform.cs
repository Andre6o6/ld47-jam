using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    public float speed;
    public Vector2 direction;
    public bool ignorePlayerGravity;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        if (ignorePlayerGravity)
            direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 movement;
        if (ignorePlayerGravity)
        {
            movement = direction * speed * Time.deltaTime;
        }
        else
        {
            movement = Physics2D.gravity.normalized * speed * Time.deltaTime;
        }

        rigid.MovePosition(rigid.position + movement);
    }
}
