using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float minJumpForce;
    public float maxJumpForce;
    public float speedInAir;
    public float accelerationTime;

    private float currentVelocityX;
    private PlayerMovement player;
    private Rigidbody2D rigid;

    private GameObject lastPlatform;
    private float normalProj;
    private float tangentProj;

    public ParticleSystem jumpParticle;

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
            rigid.velocity = player.platformNormal * maxJumpForce;
            lastPlatform = player.platform;

            jumpParticle?.Play();
        }

        if (!player.canBeControlled)
        {
            var h = Input.GetAxis("Horizontal");
            var tangent = new Vector2(player.platformNormal.y, -player.platformNormal.x);

            normalProj = Vector2.Dot(rigid.velocity, player.platformNormal);
            tangentProj = Vector2.Dot(rigid.velocity, tangent);

            var smoothTangentVelocity = Mathf.SmoothDamp(tangentProj, h * speedInAir, ref currentVelocityX, accelerationTime);
            rigid.velocity = normalProj * player.platformNormal + smoothTangentVelocity * tangent;

            if (Input.GetKeyUp(KeyCode.Space) && normalProj > minJumpForce)
            {
                rigid.velocity = minJumpForce * player.platformNormal + smoothTangentVelocity * tangent;
            }
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
        if (!player.canBeControlled && 
            ((collision.gameObject != lastPlatform && Cristal.cristalCount > 0) || 
            (collision.gameObject.tag == "Base" && Cristal.cristalCount == 0)))     //HACK for next lvl transition 
        {
            rigid.velocity = Vector2.zero;
            player.canBeControlled = true;
            lastPlatform = null;
        }
    }
}
