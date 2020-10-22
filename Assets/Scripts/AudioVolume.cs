using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    public AudioSource source;
    public FloatVariable volume;
    private float defaultVolume;
    private float currentVolume;    //FIXME this

    private void Awake()
    {
        defaultVolume = source.volume;
    }

    private void Update()
    {
        source.volume = volume.value * defaultVolume;
    }
}
