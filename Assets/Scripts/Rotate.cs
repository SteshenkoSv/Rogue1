using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    int rotateDirection = 0;

    [SerializeField]
    Rigidbody2D rbToRotate;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotateDirection = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateDirection = -1;
        }
        else 
        {
            rotateDirection = 0;
        }
    }

    private void FixedUpdate()
    {
        rbToRotate.AddTorque(rotateDirection * rotationSpeed * Time.fixedDeltaTime);
    }
}
