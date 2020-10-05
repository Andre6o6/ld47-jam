using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafParticle : MonoBehaviour
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        var force = particles.forceOverLifetime;
        force.xMultiplier = Physics2D.gravity.normalized.x;
        force.yMultiplier = Physics2D.gravity.normalized.y;
    }
}
