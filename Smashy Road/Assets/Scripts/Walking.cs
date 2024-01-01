using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public Vector3 speed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, endPosition)) > 1.0f) transform.position += speed;
        else transform.position = startPosition;

    }
}
