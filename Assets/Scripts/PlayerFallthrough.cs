using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFallthrough : MonoBehaviour
{
    private CharacterController player;
    private Collider2D platform;

    private bool canFallthrought = true;
    private bool restrictReset;

    public ParticleSystem particle;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (canFallthrought && Input.GetKeyDown(KeyCode.S))
        {
            canFallthrought = false;

            if (particle != null)
                particle.Play();

            Fallthrough();
        }
    }

    public void Fallthrough()
    {
        //Raycast to find first platform below
        var hit = Physics2D.Raycast(player.transform.position, player.gravity, Mathf.Infinity, player.platformMask);
        if (hit)
        {
            platform = hit.collider;
            platform.isTrigger = true;

            player.gravityScale = 3;   //FIXME mb need to mltply or smth, for when it is set to 0
            player.SetVelocity(Vector2.zero);
            player.grounded = false;
            player.canBeControlled = false;

            StartCoroutine(ResetAfterTime(1.5f));
        }
    }

    public void ResetFall()
    {
        canFallthrought = true;
        platform.isTrigger = false;

        player.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == platform)
        {
            restrictReset = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == platform)
        {
            ResetFall();
            restrictReset = false;
        }
    }

    private IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!canFallthrought && !restrictReset)
        {
            ResetFall();
        }
    }
}
