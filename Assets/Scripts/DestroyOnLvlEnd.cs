using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLvlEnd : MonoBehaviour
{
    void Update()
    {
        if (Cristal.cristalCount == 0)
            Destroy(this.gameObject);
    }
}
