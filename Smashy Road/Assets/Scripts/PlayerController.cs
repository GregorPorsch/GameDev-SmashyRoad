using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Drive ds;
    // public GameObject redLight;
    public bool mobileVersion;

    private float a = 0;
    private float s = 0;
    private float b = 0;

    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();
        // redLight.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (mobileVersion)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).position.x > Screen.width / 2) {
                    a = 1;
                    b = 0;
                }
                else if (Input.GetTouch(0).position.x < Screen.width / 2)
                {
                    b = 1;
                    a = 0;
                }               
            }
            else if (Input.touchCount == 2)
            {
                a = -1;
                b = 0;
            }
            else
            {
                b = 0;
                a = 0;
            }
            s = Input.acceleration.x * 1.3f;
        }
        else
        {
            s = Input.GetAxis("Horizontal");
            a = Input.GetAxis("Vertical");
            b = Input.GetAxis("Jump");
        }

        // if (b > 0.01f) redLight.SetActive(true);
        // else redLight.SetActive(false);

        ds.Go(a, s, b);
    }
}
