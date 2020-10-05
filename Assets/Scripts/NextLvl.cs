using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    public string nextLvl;
    public float time1, time2;

    public GameObject image;

    public void LoadNextLvl()
    {
        SceneManager.LoadScene(nextLvl);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadAfterTime());
    }

    public IEnumerator LoadAfterTime()
    {
        image.SetActive(true);
        yield return new WaitForSeconds(time1);
        var obj = FindObjectOfType<CastleSpawn>();
        obj.castleDown.GetComponent<SpriteRenderer>().enabled = false;
        obj.castleUp.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(time2);
        SceneManager.LoadScene(nextLvl);
    }
}
