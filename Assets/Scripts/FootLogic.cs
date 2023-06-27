using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootLogic : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private bool isAttached = false;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RollingWood"))
        {
            if (!isAttached)
            {
                Debug.Log("Foot collided with RollingWood.");
                transform.parent = collision.transform;
                isAttached = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("RollingWood"))
        {
            if (isAttached)
            {
                transform.parent = null;
                isAttached = false;
            }
        }
    }
}
