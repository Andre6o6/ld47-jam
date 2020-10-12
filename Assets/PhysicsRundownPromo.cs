using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRundownPromo : MonoBehaviour
{
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector3 origin = player.transform.position;

        var c = new Color(25, 25, 25);
        Debug.DrawRay(origin, Vector2.up, c);
        Debug.DrawRay(origin, Vector2.down, c);
        Debug.DrawRay(origin, Vector2.left, c);
        Debug.DrawRay(origin, Vector2.right, c);

        
        float h = Input.GetAxis("Horizontal");
        Debug.DrawRay(origin, player.platformNormal, Color.red);

        var tangent = new Vector2(player.platformNormal.y, -player.platformNormal.x);
        

        float a = Mathf.Min(0, Vector2.Dot(Vector2.up, h * tangent));
        Debug.DrawRay(origin, a * Vector2.up, Color.cyan);
        a = Mathf.Min(0, Vector2.Dot(Vector2.down, h * tangent));
        Debug.DrawRay(origin, a * Vector2.down, Color.cyan);
        a = Mathf.Min(0, Vector2.Dot(Vector2.left, h * tangent));
        Debug.DrawRay(origin, a * Vector2.left, Color.cyan);
        a = Mathf.Min(0, Vector2.Dot(Vector2.right, h * tangent));
        Debug.DrawRay(origin, a * Vector2.right, Color.cyan);

        Debug.DrawRay(origin, h * tangent, Color.blue);
    }
}
