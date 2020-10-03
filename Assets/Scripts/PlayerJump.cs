using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    public float speedInAir;
    public float accelerationTime;

    private float currentVelocityX;
    private PlayerMovement player;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.grounded)
        {
            player.canBeControlled = false;
            rigid.velocity = player.platformNormal * jumpForce;
        }

        if (!player.canBeControlled)
        {
            var h = Input.GetAxis("Horizontal");
            var tangent = new Vector2(player.platformNormal.y, -player.platformNormal.x);
            
            var normalProj = Vector2.Dot(rigid.velocity, player.platformNormal);
            var tangentProj = Vector2.Dot(rigid.velocity, tangent);

            var smoothTangentVelocity = Mathf.SmoothDamp(tangentProj, h * speedInAir, ref currentVelocityX, accelerationTime);
            rigid.velocity = normalProj * player.platformNormal + smoothTangentVelocity * tangent;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.canBeControlled = true;
    }
}
