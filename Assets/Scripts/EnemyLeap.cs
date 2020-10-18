using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeap : MonoBehaviour
{
    public float leapForce;
    public float waitTime;

    private CharacterController enemy;

    private GameObject lastPlatform;
    private Animator anim;

    private void Awake()
    {
        enemy = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetBool("float", false);
    }

    public void Leap(Transform target)
    {
        enemy.canBeControlled = false;
        enemy.grounded = false;
        enemy.gravityScale = 0;
        //enemy.GetComponent<EnemyController>().enabled = false;

        enemy.transform.Translate(enemy.platformNormal * 0.5f);
        anim.SetBool("float", true);
        lastPlatform = enemy.platform;

        StartCoroutine(LeapAfterTime(target));
    }

    private IEnumerator LeapAfterTime(Transform target)
    {
        yield return new WaitForSeconds(waitTime);
        Vector2 direction = target.position - transform.position;

        enemy.platformNormal = -direction.normalized;
        enemy.localVelocity = Vector2.down * leapForce;
        anim.SetBool("float", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy.canBeControlled = true;
        enemy.ResetCharacter();

        //enemy.GetComponent<EnemyController>().enabled = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!enemy.canBeControlled)
        {
            if (collision.gameObject != lastPlatform)
            {
                enemy.canBeControlled = true;
                enemy.ResetCharacter();
            }
        }
    }

    //OnTriggerEnter in killbox? Destroy? respawn after time?
}
