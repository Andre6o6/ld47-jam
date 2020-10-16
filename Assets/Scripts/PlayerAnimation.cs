using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private CharacterController player;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float vX = player.localVelocity.x;
        float vY = player.localVelocity.y;

        if (vX > 0)
        {
            sprite.flipX = true;
        }
        if (vX < 0)
        {
            sprite.flipX = false;
        }

        anim.SetFloat("vX", Mathf.Abs(vX));
        anim.SetFloat("vY", vY);
        anim.SetBool("grounded", player.grounded);
    }
}
