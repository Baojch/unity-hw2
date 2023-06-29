using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    GameObject m_player;
    Vector3 m_cameraTarget;

    const float Y_OFFSET = 1.5f;
    const float Z_OFFSET = 3.0f;

    const float MIN_X = -10;
    const float MAX_X = 10;

    const float MIN_Z = 3;
    const float MAX_Z = 8;

    float m_rotationX;
    float m_rotationY;

    float m_zOffset = Z_OFFSET;



    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // // Rotation
        // if (Input.GetButton("Fire2"))
        // {
        //     m_rotationY += Input.GetAxis("Mouse X");
        //     m_rotationX -= Input.GetAxis("Mouse Y");
        //     m_rotationX = Mathf.Clamp(m_rotationX, MIN_X, MAX_X);
        // }

        // // Zooming
        // m_zOffset -= Input.GetAxis("Mouse ScrollWheel");
        // m_zOffset = Mathf.Clamp(m_zOffset, MIN_Z, MAX_Z);

        if (Input.GetKeyDown(KeyCode.Q)|| Input.GetButtonDown("Q"))
        {
            m_rotationY -= 60;
        }
        else if (Input.GetKeyDown(KeyCode.E)|| Input.GetButtonDown("E"))
        {
            m_rotationY += 60;
        }
        if (Input.GetKeyUp(KeyCode.Q)|| Input.GetButtonUp("Q"))
        {
            m_rotationY += 60;
        }

        if (Input.GetKeyUp(KeyCode.E)|| Input.GetButtonUp("E"))
        {
            m_rotationY -= 60;
        }

        m_rotationX = 20f;

        // Y offset
        m_cameraTarget = m_player.transform.position;
        m_cameraTarget.y += Y_OFFSET;
    }

    void LateUpdate()
    {
        Vector3 cameraOffset = new Vector3(0, 0, -m_zOffset);
        Quaternion cameraRotation = Quaternion.Euler(m_rotationX, m_rotationY, 0);
        transform.position = m_cameraTarget + cameraRotation * cameraOffset;

        transform.LookAt(m_cameraTarget);
    }
}
