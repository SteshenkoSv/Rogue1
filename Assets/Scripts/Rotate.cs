using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    int rotateDirection = 0;

    [SerializeField]
    GameObject goToRotate;

    private static Rotate _instance;
    public static Rotate Instance { get { return _instance; } }

    private bool rotateAllowed;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        rotateAllowed = true;
    }

    void Update()
    {
        if (rotateAllowed) 
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
    }

    private void FixedUpdate()
    {
        if (rotateAllowed)
        {
            //rbToRotate.AddTorque(rotateDirection * rotationSpeed * Time.fixedDeltaTime);
            goToRotate.transform.Rotate(0,0, rotateDirection * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void RotateDisable() 
    {
        rotateAllowed = false;
    }
}
