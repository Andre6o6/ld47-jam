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
        
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //FIXME there is some unparenting after reparenting going on
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            if (collision.transform.parent = this.transform)
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
}
