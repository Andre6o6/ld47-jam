using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private static FrameManager _instance;
    public static FrameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FrameManager>();
                if (_instance == null)
                {
                    Debug.LogError("No frame manager");
                }
            }

            return _instance;
        }
    }

    private CharacterController player;

    public Frame[] frames;
    private int frameIndex;
    public Frame currentFrame;
    private bool loading;

    public float restartTime = 1;
    public Vector2 spawnPosition;
    public Vector2 gravityPosition;

    [HideInInspector]
    public List<Cristal> cristals = new List<Cristal>();

    public FrameCamera frameCamera;

    private bool restarting;

    public void Awake()
    {
        _instance = this;

        player = FindObjectOfType<CharacterController>();
        frameCamera = GetComponent<FrameCamera>();

        currentFrame = frames[frameIndex];
    }

    private void OnDisable()
    {
        _instance = null;
    }

    public void NextFrame()
    {
        //Throw player with gravity
        player.localVelocity = Vector2.zero;
        player.SetGravity(gravityPosition - (Vector2)player.transform.position);
        player.canBeControlled = false;

        //Start animation for every platform
        var platforms = FindObjectsOfType<Platform>();
        foreach (var p in platforms)
        {
            p.anim.SetTrigger("Destroy");
        }

        //New list for cristals 
        cristals = new List<Cristal>();

        loading = true;
    }

    public void Update()
    {
        if (loading)
        {
            if (!player.canBeControlled)
            {
                player.SetGravity(gravityPosition - (Vector2)player.transform.position);
            }
            else
            {
                loading = false;

                //Switch frames
                //TODO if no more frames - next lvl
                currentFrame.gameObject.SetActive(false);
                frameIndex += 1;
                currentFrame = frames[frameIndex];
                currentFrame.gameObject.SetActive(true);
            }
        }
    }

    public void RestartFrame()
    {
        if (!restarting)    //cuz fires multipe times
        {
            StartCoroutine(RestartAfterTime());
        }
    }

    private IEnumerator RestartAfterTime()
    {
        restarting = true;

        //Disable player
        var playerInput = player.GetComponent<PlayerInput>();
        playerInput.Despawn();
        
        yield return new WaitForSeconds(restartTime);
        restarting = false;

        //Enable player back
        playerInput.Respawn();
        player.transform.position = spawnPosition;

        //TODO enable cristals back on
        foreach (var c in cristals)
        {
            c.gameObject.SetActive(true);
        }

        //Do some stuff specific to frame
        currentFrame.onRestart?.Invoke();
    }
}
