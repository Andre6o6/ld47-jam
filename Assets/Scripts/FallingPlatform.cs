using System.Collections;
using UnityEngine;

public class FallingPlatform : Platform
{
    public float speed;

    public float returnTime;
    private Vector2 originalPosition;
    private bool falling;

    private Vector2 velocity;

    public AudioSource audioFall;
    public ParticleSystem particleFall;

    protected override void OnEnable()
    {
        base.OnEnable();
        originalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(this.transform);

            if (!falling)
            {
                falling = true;
                velocity = speed * collision.GetContact(0).normal;

                audioFall?.Play();
                if (particleFall != null)
                {
                    var force = particleFall.forceOverLifetime;
                    force.xMultiplier = Physics2D.gravity.x;
                    force.yMultiplier = Physics2D.gravity.y;
                    particleFall.Play();
                }
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (falling)
            {
                //? mb it looks better without stopping 
                //velocity = Vector2.zero;
                velocity *= 0.5f;

                anim.SetTrigger("Destroy");
                StartCoroutine(Recreate());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //FIXME make sure this fires on platform resetting and on switching to next frame
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            anim.SetTrigger("Destroy");
            StartCoroutine(Recreate());
        }
    }

    private IEnumerator Recreate()
    {
        yield return new WaitForSeconds(returnTime);
        velocity = Vector2.zero;
        transform.position = originalPosition;
        anim.SetTrigger("Create");
        falling = false;
    }
}
