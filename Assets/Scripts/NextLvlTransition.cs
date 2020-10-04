using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvlTransition : MonoBehaviour
{
    public static bool restarted;

    public Vector2 spawnPosition;
    public string nextLvl;

    private bool loading;
    private PlayerMovement player;

    public void LoadNextLvl()
    {
        loading = true;
        restarted = false;

        player = FindObjectOfType<PlayerMovement>();
        //HACK jump a litle to be able to fire OnCollisionEnter
        player.GetComponent<Rigidbody2D>().velocity += 1 * player.platformNormal.normalized;
        player.SetGravity(spawnPosition - (Vector2)player.transform.position);
        player.canBeControlled = false;

        var platforms = FindObjectsOfType<Platform>();
        foreach (var p in platforms)
        {
            p.anim.SetTrigger("Destroy");
        }

        print("Loading " + nextLvl);
    }

    public void Update()
    {
        if (loading)
        {
            if (!player.canBeControlled)
            {
                player.SetGravity(spawnPosition - (Vector2)player.transform.position);
            }
            else
            {
                loading = false;
                SceneManager.LoadScene(nextLvl);
                print("Loaded");
            }
        }
    }

    public void RestartLvl()
    {
        Cristal.cristalCount = 0;

        var player = FindObjectOfType<PlayerMovement>();
        Destroy(player.gameObject);

        restarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
