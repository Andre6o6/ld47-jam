using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float minJumpForce;
    public float maxJumpForce;
    public float speedInAir;
    public float accelerationTime;

    private float currentVelocityX;
    private CharacterController player;

    private GameObject lastPlatform;

    public ParticleSystem jumpParticle;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && player.grounded)
        {
            player.canBeControlled = false;
            player.localVelocity.y = maxJumpForce;
            lastPlatform = player.platform;

            if (jumpParticle != null)
                jumpParticle.Play();
        }

        if (!player.canBeControlled)
        {
            var h = Input.GetAxis("Horizontal");

            var smoothTangentVelocity = Mathf.SmoothDamp(player.localVelocity.x, h * speedInAir, ref currentVelocityX, accelerationTime);
            player.localVelocity.x = smoothTangentVelocity;

            if (Input.GetButtonUp("Jump") && player.localVelocity.y > minJumpForce)
            {
                player.localVelocity.y = minJumpForce;
            }
        }
    }

    private void ResetJump(Collision2D collision)
    {
        player.SetVelocity(Vector2.zero);
        player.localVelocity = Vector2.zero;
        player.SetGravity(-collision.GetContact(0).normal);

        player.canBeControlled = true;
        lastPlatform = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!player.canBeControlled)
        {
            ResetJump(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!player.canBeControlled &&
            ((collision.gameObject != lastPlatform && Cristal.cristalCount > 0) ||  //HACK for jumping by the wall
            (collision.gameObject.tag == "Base" && Cristal.cristalCount == 0)))     //HACK for next lvl transition 
        {
            ResetJump(collision);
        }
    }
}
