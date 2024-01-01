using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public WheelCollider[] WCs;
    public GameObject[] Wheels;
    public float torque = 200;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 500;
    public Rigidbody rb;
    public float steerSensitivity = 0.8f;
    public float maxSpeed = 30.0f;
    public Vector3 COM;

    public TrailRenderer[] tireMarks;
    bool tireMarksFlag;
    public AudioSource skidClip;
    public ParticleSystem smokePrefab;
    ParticleSystem[] skidSmoke = new ParticleSystem[4];
    public GameObject backLights;

    public float currentSpeed { get { return rb.velocity.magnitude; } }

    void Start()
    {
        skidClip.volume = Globals.sfxVolume;
        rb.centerOfMass = COM;
        backLights.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            skidSmoke[i] = Instantiate(smokePrefab);
            skidSmoke[i].Stop();
        }
    }
    
    public void Go(float accel, float steer, float brake)
    {

        if (brake > 0) backLights.SetActive(true);
        else backLights.SetActive(false);

        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle * steerSensitivity;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        float thrustTorque = accel * torque;

        if (rb.velocity.magnitude > maxSpeed) thrustTorque = 0;

        if (Mathf.Abs(steer) > 0.3f)
        {
            thrustTorque /= 2;
            if (rb.velocity.magnitude > 25.0f) brake = 0.5f * maxBrakeTorque;
        }

        for (int i = 0; i < 4; i++)
        {
            WCs[i].motorTorque = thrustTorque;

            if (i < 2) WCs[i].steerAngle = steer;
            else WCs[i].brakeTorque = brake;

            Quaternion quat;
            Vector3 position;
            WCs[i].GetWorldPose(out position, out quat);
            Wheels[i].transform.position = position;
            Wheels[i].transform.localRotation = quat;
        }

        if (brake > 0) startSkid();
        else stopSkid();
    }

    private void startSkid()
    {
        if (tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
            skidClip.Play();
        }

        for (int i = 0; i < 4; i++)
        {
            skidSmoke[i].transform.position = WCs[i].transform.position - WCs[i].transform.up * WCs[i].radius;
            skidSmoke[i].Emit(1);
        }

        tireMarksFlag = true;
    }

    private void stopSkid()
    {
        if (!tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
            skidClip.Stop();
        }

        tireMarksFlag = false;
    }
}
