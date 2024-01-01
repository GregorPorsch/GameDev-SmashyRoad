using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoint : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            rb.isKinematic = false;
            Invoke("delete", 5);
        }
    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
