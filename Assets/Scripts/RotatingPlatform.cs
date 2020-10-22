using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Platform
{
    public float rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //FIXME
        //Instead of parenting call player.Move(gravity), to move with platform and update normal
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.SetParent(null);
            //HACK because reparenting breaks DDOL
            if (collision.gameObject.GetComponent<DontDestroyOnLoad>() != null)
            {
                DontDestroyOnLoad(collision.gameObject);
            }
        }
    }
}
