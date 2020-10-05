using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToNormal : MonoBehaviour
{
    public PlayerMovement player;
    public EnemyController enemy;

    private void Update()
    {
        float angle = 0;
        if (player != null)
        {
            angle = Vector2.SignedAngle(Vector2.up, player.platformNormal);
        }
        else if (enemy != null)
        {
            angle = Vector2.SignedAngle(Vector2.up, enemy.platformNormal);
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
