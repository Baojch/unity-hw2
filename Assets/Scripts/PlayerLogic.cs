using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    const float GRAVITY = -1.0f;
    const float MOVEMENT_SPEED = 5.0f;
    const float JUMPFORCE = 5.0f;
    
    //moving
    float m_horizontalInput;
    float m_verticalInput;
    //jumping
    int m_isJumping;
    int m_falling;
    int changing_m_isjumping;//first jump height
    bool secondJump_Lock;

    CharacterController m_characterController;

    Vector3 m_movement;

    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        changing_m_isjumping = 4;
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        m_animator.SetFloat("Horizontal", m_horizontalInput);
        m_animator.SetFloat("Vertical", m_verticalInput);

        if(Input.GetKeyDown(KeyCode.Space) && m_characterController.isGrounded){
            m_isJumping += changing_m_isjumping;
            Debug.Log("m_isJumping");
            secondJump_Lock = false;
        }else
        { //on the ground
            m_movement.y =+ GRAVITY * Time.deltaTime;   
        }
        // judge second jump
        if (Input.GetKeyDown(KeyCode.Space) && !m_characterController.isGrounded && !secondJump_Lock)
        {
            Debug.Log("Second Jump!!!"); 
            m_isJumping += changing_m_isjumping - 1;
            secondJump_Lock = true;
        }
    }

    void FixedUpdate()
    {
        m_movement.x = m_horizontalInput * MOVEMENT_SPEED * Time.deltaTime;
        m_movement.z = m_verticalInput * MOVEMENT_SPEED * Time.deltaTime;

        //jumping logic starts here
        if (m_isJumping != 0)
        {
            m_movement.y += JUMPFORCE * m_isJumping * Time.deltaTime;
            m_isJumping--;

            if(m_isJumping == 1)
            {
                m_falling = 99;
            }
        }
        else{
            // falling
            if (m_falling != 0 && !m_characterController.isGrounded)
            {
                m_movement.y += GRAVITY * Time.deltaTime * (100 - m_falling) / 2;
                m_falling--;
            }
            else // On ground
            {
                m_falling = 100;
                m_movement.y += GRAVITY * Time.deltaTime;
            }
        }
        //jumping logic ends here
        m_characterController.Move(m_movement);
    }

    void Footstep(int footIndex)
    {
        Debug.Log("Footstep: " + footIndex);
    }
}
