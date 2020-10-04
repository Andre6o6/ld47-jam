using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var velocity = rigid.velocity;

        //var tangent = new Vector2(player.platformNormal.y, -player.platformNormal.x);
        float vX = Input.GetAxis("Horizontal");
        float vY = Vector2.Dot(velocity, player.platformNormal);

        if (vX > 0)
        {
            sprite.flipX = true;
        }
        if (vX < 0)
        {
            sprite.flipX = false;
        }

        anim.SetFloat("vX", Mathf.Abs(vX));
        anim.SetFloat("vY", vY);
        anim.SetBool("grounded", player.grounded);
    }
}
