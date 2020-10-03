using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public static int cristalCount;

    private void Start()
    {
        cristalCount += 1;
        print(cristalCount.ToString() + " cristals");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            cristalCount -= 1;
            Destroy(this.gameObject);

            if (cristalCount == 0)
            {
                var nextLvl = FindObjectOfType<NextLvlTransition>();
                nextLvl.LoadNextLvl();
            }
        }
    }
}
