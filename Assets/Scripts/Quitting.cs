using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quitting : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    private IEnumerator QuitAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        Quit();
    }

    public void Quit()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            var player = FindObjectOfType<PlayerInput>();
            Destroy(player.gameObject);

            SceneManager.LoadScene("menu");
        }
        else
        {
            Application.Quit();
        }
    }
}
