using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDecoCar : MonoBehaviour
{
    public Rigidbody rb;
    public AIController aiController;
    float lastTimeChecked;

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0.1f) lastTimeChecked = Time.time;
        if (Time.time > lastTimeChecked + 3) RightCar();
    }

    public void RightCar()
    {
        this.transform.position = aiController.circuit.waypoints[aiController.currentWP].transform.position;
        this.transform.position += Vector3.up;
        this.transform.rotation = Quaternion.LookRotation(aiController.circuit.waypoints[aiController.currentWP].transform.forward);
    }
}
