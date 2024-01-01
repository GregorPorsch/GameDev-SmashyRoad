using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceNMController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform policeCar;
    public Rigidbody rb;
    public float maxDistance;
    public float minDistance;
    public float normalSpeed;
    public float slowspeed;
    public float fastSpeed;
    GameObject target;

    private void Start()
    {
        target = GameObject.Find("PlayerCarBody");
    }

    // Update is called once per frame
    void Update()
    {
        if (policeCar != null)
        {
            agent.destination = target.transform.position;

            if (Vector3.Distance(transform.position, policeCar.position) > maxDistance) GetComponent<NavMeshAgent>().speed = slowspeed;
            else if (Vector3.Distance(transform.position, policeCar.position) < minDistance) GetComponent<NavMeshAgent>().speed = fastSpeed;
            else GetComponent<NavMeshAgent>().speed = normalSpeed;

            // if (rb.velocity.magnitude < 1) GetComponent<NavMeshAgent>().speed = 0;
        }
        else
        {
            Debug.Log("No police Car");
        }
    }
}
