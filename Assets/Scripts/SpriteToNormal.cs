using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToNormal : MonoBehaviour
{
    public PlayerMovement player;

    private void Update()
    {
        float angle = Vector2.SignedAngle(Vector2.up, player.platformNormal);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
