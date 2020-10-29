using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCristal : MonoBehaviour
{
    public CharacterController enemy;
    public int cristalTresh;

    public bool useThisCristal;

    private void Update()
    {
        //TODO get cristal event
        if (!enemy.gameObject.activeInHierarchy &&
            Cristal.cristalCount > 0 && 
            Cristal.cristalCount <= cristalTresh)
        {
            if (!useThisCristal)
            {
                SpawnEnemy();
            }
            else
            {
                enemy.transform.position = this.transform.position;
                enemy.gameObject.SetActive(true);
            }

            this.gameObject.SetActive(false);
        }
    }

    private void SpawnEnemy()
    {
        var cristalsLeft = FindObjectsOfType<Cristal>();
        int idx = Random.Range(0, cristalsLeft.Length);

        enemy.transform.position = cristalsLeft[idx].transform.position;
        enemy.gameObject.SetActive(true);

        cristalsLeft[idx].PopCristal();
    }
}
