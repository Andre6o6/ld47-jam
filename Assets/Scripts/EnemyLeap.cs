using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeap : MonoBehaviour
{
    public float leapForce;
    public float waitTime;

    private EnemyController enemy;
    private Rigidbody2D rigid;

    private GameObject lastPlatform;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
    }

    public void Leap(Transform target)
    {
        enemy.canBeControlled = false;
        enemy.transform.Translate(enemy.platformNormal * 0.5f);
        lastPlatform = enemy.platform;

        StartCoroutine(LeapAfterTime(target));
    }

    private IEnumerator LeapAfterTime(Transform target)
    {
        yield return new WaitForSeconds(waitTime);
        Vector2 direction = target.position - transform.position;
        rigid.velocity = direction.normalized * leapForce;
        enemy.platformNormal = -direction.normalized ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy.canBeControlled = true;
        enemy.localVelocity = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!enemy.canBeControlled)
        {
            if (collision.gameObject != lastPlatform)
            {

                enemy.canBeControlled = true;
                enemy.localVelocity = Vector2.zero;
                rigid.velocity = Vector2.zero;
            }
        }
    }

    //OnTriggerEnter in killbox? Destroy? respawn after time?

}
