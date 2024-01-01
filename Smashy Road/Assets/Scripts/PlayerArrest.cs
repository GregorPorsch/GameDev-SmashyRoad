using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrest : MonoBehaviour
{
    // public bool arrested;

    GameObject canvas;
    GameSceneCanvas gameSceneCanvasScript;
    Stopwatch stopwatch;
    float contactLength;

    private void Start()
    {
        stopwatch = this.GetComponent<Stopwatch>();
        contactLength = 0;
        canvas = GameObject.Find("Canvas");
        gameSceneCanvasScript = canvas.GetComponent<GameSceneCanvas>();
    }

    private void Update()
    {
        contactLength = stopwatch.GetSeconds();

        if (contactLength > 3)
        {
            Time.timeScale = 0;
            // arrested = true;
            gameSceneCanvasScript.dead();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            stopwatch.Begin();
            Debug.Log("Collision");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            stopwatch.Reset();
            Debug.Log("Collision Exit");
        }
    }
}
