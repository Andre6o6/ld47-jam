using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quitting : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO sound pitch shift
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }

    private IEnumerator QuitAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        Quit();
    }

    public void ToMenu()
    {
        var ddol = FindObjectsOfType<DontDestroyOnLoad>();
        foreach (var obj in ddol)
        {
            Destroy(obj.gameObject);
        }

        SpeedrunTimer.started = false;
        var t = FindObjectOfType<SpeedrunTimer>();
        if (t != null)
        {
            t.time.value = 0;
        }

        SceneManager.LoadScene("menu");
    }

    public void Quit()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            ToMenu();
        }
        else
        {
            Application.Quit();
        }
    }
}
