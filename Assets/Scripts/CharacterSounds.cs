using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public int randomFrom, randomTo;

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
    }

    public void PlaySound(int i)
    {
        source.PlayOneShot(sounds[i]);
    }

    public void PlayRandomSound()
    {
        source.PlayOneShot(sounds[Random.Range(randomFrom, randomTo)]);
    }
}
