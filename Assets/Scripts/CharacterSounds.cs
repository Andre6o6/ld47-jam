using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public int randomFrom, randomTo;

    public FloatVariable volume;
    private float defaultVolume;
    private float currentVolume;    //TODO set this in onSliderValueChanged event

    private void Awake()
    {
        source = GetComponentInChildren<AudioSource>();
        defaultVolume = source.volume;
    }

    private void SetVolume()
    {
        source.volume = volume.value * defaultVolume;
    }

    public void PlaySound(int i)
    {
        SetVolume();
        source.PlayOneShot(sounds[i]);
    }

    public void PlayRandomSound()
    {
        SetVolume();
        source.PlayOneShot(sounds[Random.Range(randomFrom, randomTo)]);
    }
}
