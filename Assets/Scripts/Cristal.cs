using UnityEngine;

public class Cristal : MonoBehaviour
{
    public static int cristalCount;
    public ParticleSystem collectParticles;
    public AudioSource audiosource;

    private void OnEnable()
    {
        cristalCount += 1;

        if (!FrameManager.instance.cristals.Contains(this))
        {
            FrameManager.instance.cristals.Add(this);
        }
    }

    private void OnDisable()
    {
        cristalCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PopCristal();

            if (cristalCount == 0)
            {
                //var nextLvl = FindObjectOfType<NextLvlTransition>();
                //nextLvl.LoadNextLvl();
                FrameManager.instance.NextFrame();
            }
        }
    }

    public void PopCristal()
    {
        var particles = Instantiate(collectParticles, transform.position, Quaternion.identity);
        particles?.Play();
        Destroy(particles.gameObject, 0.5f);

        var audio = Instantiate(audiosource, transform.position, Quaternion.identity);
        audio.Play();
        Destroy(audio.gameObject, audiosource.clip.length);

        gameObject.SetActive(false);
    }
}
