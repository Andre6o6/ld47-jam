using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject cloud;
    public Sprite[] sprites;

    public float xPos;
    public float yPosMin, yPosMax;
    public float minScale, maxScale;
    public float minSpeed, maxSpeed;
    public float minTime, maxTime;

    private IEnumerator Start()
    {
        DontDestroyOnLoad(this);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            var obj = Instantiate(cloud);
            obj.transform.position = new Vector2(xPos, Random.Range(yPosMin, yPosMax));
            obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            obj.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), 1);

            obj.GetComponent<Rigidbody2D>().velocity = Random.Range(minSpeed, maxSpeed) * Vector2.left;

            obj.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

            obj.transform.SetParent(this.transform, true);
        }
    }

}
