using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sack : MonoBehaviour
{
    AudioSource collectSound;
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    public GameObject spotLight;
    public GameObject bagMarker;

    private void Start()
    {
        collectSound = GetComponent<AudioSource>();
        collectSound.volume = Globals.sfxVolume;
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            collectSound.Play();
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            spotLight.SetActive(false);
            bagMarker.SetActive(false);
            Globals.bags++;
            Invoke("destroy", 2);
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
