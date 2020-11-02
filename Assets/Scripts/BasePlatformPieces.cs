using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformPieces : MonoBehaviour
{
    public CharacterController player;
    public float gravityScale = 1;
    public Rigidbody2D[] pieces;
    public ParticleSystem particleFall;
    public AudioSource audioFall;

    private bool volitile;

    public void SetVolitile()
    {
        volitile = true;
    }

    public void BreakIntoPieces()
    {
        volitile = false;

        //Disable sprite
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        //Send pieces flying
        foreach (var rigid in pieces)
        {
            rigid.velocity = gravityScale * Random.Range(0f, 1f) * Vector2.left * player.gravityForce;
            rigid.GetComponent<Animator>().SetTrigger("Destroy");
        }

        PlayParticles();
        audioFall?.Play();

        Destroy(this.gameObject, 5);
    }

    public void PlayParticles()
    {
        if (particleFall != null)
        {
            var force = particleFall.forceOverLifetime;
            force.xMultiplier = player.gravity.x;
            force.yMultiplier = player.gravity.y;
            particleFall.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (volitile)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayParticles();
            }
        }
    }
}
