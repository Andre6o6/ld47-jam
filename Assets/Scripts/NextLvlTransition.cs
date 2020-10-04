using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvlTransition : MonoBehaviour
{
    public static bool restarted;

    public Vector2 spawnPosition;
    public string nextLvl;
    public float restartTime = 2;

    private bool loading;
    private PlayerMovement player;

    public void LoadNextLvl()
    {
        loading = true;
        restarted = false;

        player = FindObjectOfType<PlayerMovement>();
        //HACK jump a litle to be able to fire OnCollisionEnter
        player.GetComponent<Rigidbody2D>().velocity += 2 * player.platformNormal.normalized;
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
        player = FindObjectOfType<PlayerMovement>();
        player.enabled= false;
        player.GetComponent<PlayerJump>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;

        StartCoroutine(RestartAfterTime());
    }

    private IEnumerator RestartAfterTime()
    {
        yield return new WaitForSeconds(restartTime);

        Cristal.cristalCount = 0;
        restarted = true;
        Destroy(player.gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
