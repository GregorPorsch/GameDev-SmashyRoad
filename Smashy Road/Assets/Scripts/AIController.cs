using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Circuit circuit;
    public Rigidbody rb;
    Drive ds;
    public float steeringSennsitivity = 0.05f;
    Vector3 target;
    public int currentWP = 0;

    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();
        target = circuit.waypoints[currentWP].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            Vector3 localTarget = ds.rb.gameObject.transform.InverseTransformPoint(target);
            float distanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            float steer = Mathf.Clamp(targetAngle * steeringSennsitivity, -1, 1) * Mathf.Sign(ds.currentSpeed);
            float accel = 1f;
            float brake = 0;

            if (Mathf.Abs(steer) > 0.1f && rb.velocity.magnitude > 10) brake = 0.7f;
            else brake = 0;

            ds.Go(accel, steer, brake);

            if (distanceToTarget < 5)
            {
                currentWP++;
                if (currentWP >= circuit.waypoints.Length)
                    currentWP = 0;
                target = circuit.waypoints[currentWP].transform.position;
            }
        }
    }
}