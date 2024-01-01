using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePolice : MonoBehaviour
{
    Stopwatch stopwatch;
    float contactLength;
    PoliceSpawn policeSpawnScript;
    public Rigidbody rb;
    int contactLayer;

    private void Start()
    {
        policeSpawnScript = GameObject.FindGameObjectWithTag("gameManager").GetComponent<PoliceSpawn>();
        stopwatch = this.GetComponent<Stopwatch>();
        contactLength = 0;
    }

    private void Update()
    {
        contactLength = stopwatch.GetSeconds();

        if (contactLength > 3 && gameObject != null)
        {
            Destroy(transform.parent.gameObject.transform.parent.gameObject);
            policeSpawnScript.spawn(policeSpawnScript.electSpawnArea());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 7)
        {
            contactLayer = collision.gameObject.layer;
            // Debug.Log(contactLayer);
            stopwatch.Begin();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 7)
        {
            if (collision.gameObject.layer == contactLayer)
            {
                stopwatch.Reset();
            }
        }
    }
}
