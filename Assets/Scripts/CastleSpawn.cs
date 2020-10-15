using UnityEngine;

public class CastleSpawn : MonoBehaviour
{
    public GameObject castleUp, castleDown;
    public Collider2D col;
    public LayerMask playerMask;

    private void OnEnable()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = playerMask;

        Collider2D[] results = new Collider2D[1];
        int count = Physics2D.OverlapCollider(col, filter, results);

        if (count == 0)
        {
            castleUp.SetActive(true);
        }
        else
        {
            castleDown.SetActive(true);
        }

        col.enabled = false;
        Cristal.cristalCount = 1000;    //HACK cuz when 0 -> can't jump on base platform
    }
}
