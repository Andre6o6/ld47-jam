using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticles : MonoBehaviour
{
    public ParticleSystem[] particles;

    public void PlayParticles(int index)
    {
        particles[index].Play();
    }
}
