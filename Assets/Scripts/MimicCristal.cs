using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCristal : MonoBehaviour
{
    public EnemyController enemy;
    public int cristalTresh;
    public LayerMask obstacleMask;

    private void Update()
    {
        if (!enemy.gameObject.activeInHierarchy &&
            Cristal.cristalCount > 0 && 
            Cristal.cristalCount <= cristalTresh)
        {
            SpawnEnemy();
            this.gameObject.SetActive(false);
        }
    }

    private void SpawnEnemy()
    {
        var cristalsLeft = FindObjectsOfType<Cristal>();
        int idx = Random.Range(0, cristalsLeft.Length);

        enemy.transform.position = cristalsLeft[idx].transform.position;
        enemy.gameObject.SetActive(true);

        if (Physics2D.Raycast(enemy.transform.position, Vector2.down, 1, obstacleMask))
        {
            enemy.SetGravity(Vector2.down);
        }
        else if  (Physics2D.Raycast(enemy.transform.position, Vector2.up, 1, obstacleMask))
        {
            enemy.SetGravity(Vector2.up);
        }
        else if (Physics2D.Raycast(enemy.transform.position, Vector2.left, 1, obstacleMask))
        {
            enemy.SetGravity(Vector2.left);
        }
        else if (Physics2D.Raycast(enemy.transform.position, Vector2.right, 1, obstacleMask))
        {
            enemy.SetGravity(Vector2.right);
        }

        cristalsLeft[idx].PopCristal();
    }
}
