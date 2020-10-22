using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    public bool edgeKillbox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //FIXME Hack. Mb I need to set up an Event on frame end or have killbox in every platform
        if (Cristal.cristalCount == 0 && !edgeKillbox)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Animator>()?.SetBool("Die", true);
            FrameManager.instance.RestartFrame();
        }
    }
}
