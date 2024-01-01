using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageController : MonoBehaviour
{
    public Rigidbody rb;
    float playerHealth = 800;
    GameObject gameSceneCanvas;
    GameSceneCanvas canvasScript;
    AudioSource crashSound;
    public static Vector3 playerPosition;

    private void Start()
    {
        gameSceneCanvas = GameObject.Find("Canvas");
        canvasScript = gameSceneCanvas.GetComponent<GameSceneCanvas>();
        crashSound = GetComponent<AudioSource>();
        crashSound.volume = Globals.sfxVolume;
    }

    private void Update()
    {
        playerPosition = rb.transform.position;
        checkHealth();
        if (playerHealth <= 100)
        {
            GetComponent<Animator>().Play("HealthBar");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 7)
        {
            if (rb.velocity.magnitude > 5f)
            {
                playerHealth -= rb.velocity.magnitude * 0.5f;
                crashSound.Play();
                Debug.Log("Damage: " + rb.velocity.magnitude * 0.5f);
            }
            canvasScript.setHealth(playerHealth);
        }else if (collision.gameObject.tag == "water")
        {
            canvasScript.dead();
        }
    }

    public void checkHealth()
    {
        if (playerHealth <= 0)
        {
            canvasScript.dead();
        }
    }
}
