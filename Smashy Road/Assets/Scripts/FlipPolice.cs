using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPolice : MonoBehaviour
{
    public GameObject navMeshAgent;
    Rigidbody rb;
    float lastTimeChecked;
    bool objectContact;
    Vector3 lastPosition;
    float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        objectContact = false;
        rb = this.GetComponent<Rigidbody>();
        lastPosition = navMeshAgent.transform.position;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 1) lastTimeChecked = Time.time;
        if (Time.time > lastTime + 3 && objectContact == false)
        {
            lastPosition = navMeshAgent.transform.position;
            lastTime = Time.time;
        }
        if (Time.time > lastTimeChecked + 2 && objectContact) RightCar();
    }

    void RightCar()
    {
        navMeshAgent.transform.position = lastPosition;
        this.transform.position = lastPosition;
        this.transform.position += Vector3.up;
        this.transform.rotation = Quaternion.LookRotation(navMeshAgent.transform.forward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11) objectContact = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 11) objectContact = false;
    }
}
