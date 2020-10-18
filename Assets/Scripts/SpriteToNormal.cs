using UnityEngine;

public class SpriteToNormal : MonoBehaviour
{
    public CharacterController character;

    private void Update()
    {
        if (character != null)
        {
            float angle = Vector2.SignedAngle(Vector2.up, character.platformNormal);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
