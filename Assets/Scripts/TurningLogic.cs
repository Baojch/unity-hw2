using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningLogic : MonoBehaviour
{
    float spinSpeedUp = 100f;
    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // spin
        transform.Rotate(Vector3.forward, spinSpeedUp * Time.deltaTime);
    }
    public float getSpinSpeedUp()
    {
        return spinSpeedUp;
    }
    //the following no use to collide with player
    // private void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Object collided with"+ collision.gameObject.name);
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Object collided with player.");
    //     }
    // }
}
