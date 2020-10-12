using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpeedrunTimer : MonoBehaviour
{
    public static bool started;

    public TextMeshProUGUI textbox;
    public FloatVariable time;

    void Start()
    {
        if (!started)
        {
            this.gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "credits")
        {
            textbox.text = Format();
            started = false;
        }
    }

    void Update()
    {
        if (started)
        {
            time.value += Time.deltaTime;
            textbox.text = Format();
        }
    }

    public string Format()
    {
        int min = (int)time.value / 60;
        float sec = time.value - 60 * min;
        return string.Format("{0:D2}:{1:00.0}", min, sec);
    }

    public void StartTimer()
    {
        if (this.isActiveAndEnabled)
        {
            time.value = 0;
            started = true;
        }
    }
}
