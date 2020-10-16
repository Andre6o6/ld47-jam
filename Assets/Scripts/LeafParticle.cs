using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafParticle : MonoBehaviour
{
    private ParticleSystem particles;
    private CharacterController player;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        player = FindObjectOfType<PlayerInput>().GetComponent<CharacterController>();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        var force = particles.forceOverLifetime;
        force.xMultiplier = player.gravity.normalized.x;
        force.yMultiplier = player.gravity.normalized.y;
    }
}
