using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPTurningIn : MonoBehaviour
{
    float spinSpeedUp = 80f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // spin
        transform.Rotate(-Vector3.up, spinSpeedUp * Time.deltaTime);
    }
    public float getSpinSpeedUp()
    {
        return spinSpeedUp;
    }
}
