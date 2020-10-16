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

}
