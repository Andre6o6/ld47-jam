using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : Platform
{
    public float speed;

    public float returnTime;
    private Vector2 originalPosition;
    private bool falling;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        if (NextLvlTransition.restarted)
        {
            anim.SetTrigger("Create");
        }

        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!falling)
            {
                collision.transform.SetParent(this.transform);

                falling = true;

                //var player = collision.gameObject.GetComponent<PlayerMovement>();
                //FIXME may collide on vierd angle and script order will break it
                //So maybe check contact's normal

                rigid.velocity = speed * collision.GetContact(0).normal;
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (falling)
            {
                anim.SetTrigger("Destroy");
                StartCoroutine(Recreate());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
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
        rigid.velocity = Vector2.zero;
        transform.position = originalPosition;
        anim.SetTrigger("Create");
        falling = false;
    }
}
