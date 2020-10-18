using UnityEngine;

public class CompositePlatform : MonoBehaviour
{
    //TODO onNextFrame Event
    private void Update()
    {
        if (Cristal.cristalCount == 0)
        {
            DisableColliders();
        }
    }

    public void DisableColliders()
    {
        var cols = GetComponents<Collider2D>();
        foreach (var col in cols)
        {
            col.enabled = false;    
        }
    }
}
