using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    GameObject m_player;
    Vector3 m_cameraTarget;

    const float Y_OFFSET = 1.5f;
    const float Z_OFFSET = 5.0f;

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
        // Rotation
        if (Input.GetButton("Fire2"))
        {
            m_rotationY += Input.GetAxis("Mouse X");
            m_rotationX -= Input.GetAxis("Mouse Y");
            m_rotationX = Mathf.Clamp(m_rotationX, MIN_X, MAX_X);
        }

        // Zooming
        m_zOffset -= Input.GetAxis("Mouse ScrollWheel");
        m_zOffset = Mathf.Clamp(m_zOffset, MIN_Z, MAX_Z);

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
