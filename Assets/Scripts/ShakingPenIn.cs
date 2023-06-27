using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingPenIn : MonoBehaviour
{
    float rotationSpeed = -40f;
    float rotationAngle = 45f;

    Quaternion startRotation;
    Quaternion leftRotation;
    Quaternion rightRotation;


    // Start is called before the first frame update
    void Start()
    {
        leftRotation = Quaternion.Euler(0f, 0f, -rotationAngle);
        rightRotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        if(transform.rotation.z > rightRotation.z)
        {
            rotationSpeed = -rotationSpeed;
        }
        else if(transform.rotation.z < leftRotation.z)
        {
            rotationSpeed = -rotationSpeed;
        }
    }
}
