using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public static int cristalCount;
    public ParticleSystem collectParticles;
    public AudioSource audiosource;

    private void Start()
    {
        cristalCount += 1;
        print(cristalCount.ToString() + " cristals");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PopCristal();

            if (cristalCount == 0)
            {
                var nextLvl = FindObjectOfType<NextLvlTransition>();
                nextLvl.LoadNextLvl();
            }
        }
    }

    public void PopCristal()
    {
        collectParticles.transform.SetParent(null);
        collectParticles?.Play();
        Destroy(collectParticles.gameObject, 0.5f);

        audiosource.transform.SetParent(null);
        audiosource.Play();
        Destroy(audiosource.gameObject, audiosource.clip.length);

        cristalCount -= 1;
        Destroy(this.gameObject);
    }
}
