using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController player;
    public float movementSpeed;

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        if (player.canBeControlled)
            player.localVelocity.x = h * movementSpeed;
    }

    public void Despawn()
    {
        player.gravityScale = 0;
        GetComponent<PlayerJump>().enabled = false;
        player.enabled = false;
    }

    public void Respawn()
    {
        player.enabled = true;
        player.ResetCharacter();

        GetComponent<PlayerJump>().enabled = true;
        GetComponent<Animator>()?.SetBool("Die", false);
    }
}
