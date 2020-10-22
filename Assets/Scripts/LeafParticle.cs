using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafParticle : MonoBehaviour
{
    private ParticleSystem particles;
    private CharacterController player;

    public float gravityScale = 1;

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
        force.xMultiplier = gravityScale * player.gravity.normalized.x;
        force.yMultiplier = gravityScale * player.gravity.normalized.y;
    }
}
