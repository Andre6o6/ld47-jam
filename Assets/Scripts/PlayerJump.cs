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

    private GameObject lastPlatform;
    private float normalProj;
    private float tangentProj;

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
            lastPlatform = player.platform;
        }

        if (!player.canBeControlled)
        {
            var h = Input.GetAxis("Horizontal");
            var tangent = new Vector2(player.platformNormal.y, -player.platformNormal.x);

            normalProj = Vector2.Dot(rigid.velocity, player.platformNormal);
            tangentProj = Vector2.Dot(rigid.velocity, tangent);

            var smoothTangentVelocity = Mathf.SmoothDamp(tangentProj, h * speedInAir, ref currentVelocityX, accelerationTime);
            rigid.velocity = normalProj * player.platformNormal + smoothTangentVelocity * tangent;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!player.canBeControlled)
        {
            rigid.velocity = Vector2.zero;
            player.canBeControlled = true;
            lastPlatform = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print(normalProj);

        if (!player.canBeControlled && 
            lastPlatform != null && 
            collision.gameObject != lastPlatform)
        {
            rigid.velocity = Vector2.zero;
            player.canBeControlled = true;
            lastPlatform = null;
        }
    }
}
