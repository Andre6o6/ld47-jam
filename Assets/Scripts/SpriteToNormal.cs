using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToNormal : MonoBehaviour
{
    public CharacterController character;
    public EnemyController enemy;

    private void Update()
    {
        float angle = 0;
        if (character != null)
        {
            angle = Vector2.SignedAngle(Vector2.up, character.platformNormal);
        }
        else if (enemy != null)
        {
            angle = Vector2.SignedAngle(Vector2.up, enemy.platformNormal);
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
